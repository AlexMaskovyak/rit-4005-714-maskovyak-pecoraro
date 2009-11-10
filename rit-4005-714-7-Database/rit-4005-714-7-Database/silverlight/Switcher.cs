using System;

namespace ATS.Database {
  /// <summary> adds an eventhandler to a queue. </summary>
  public delegate void SetClick (EventHandler e);
  /// <summary> controller for one or more databases. </summary>
  public class Switcher {
    /// <summary> connected to each model. </summary>
    protected readonly Controller[] controller;
    /// <summary> current controller (preserved in session, etc.). </summary>
    /// <remarks> This could be a local variable in <c>Connect</c>;
    ///   however, it gets lost if used by a web page. </remarks>
    protected int current = 0;
    /// <summary> associated with each model. </summary>
    protected readonly string[] name;
    /// <summary> create and connect controllers. </summary>
    /// <param name="enable"> controls user interaction in view. </param>
    /// <param name="io"> current/size/search/enter/remove fields. </param>
    /// <param name="click"> add to eventhandlers, null to skip. </param>
    /// <param name="bg"> for sequential background execution. </param>
    /// <param name="nameModel"> one or more name/db model pairs. </param>
    public Switcher (Enable enable, IAccess[] io, SetClick[] click, WorkQueue bg, params object[] nameModel)
      : this(bg, nameModel) {
      Connect(enable, io, click);
    }
    /// <summary> create controllers. </summary>
    /// <param name="bg"> for sequential background execution. </param>
    /// <param name="nameModel"> one or more name/db model pairs. </param> 
    public Switcher (WorkQueue bg, params object[] nameModel) {
      controller = new Controller[nameModel.Length/2];
      name = new string[controller.Length];
      for (int n = 0; n < controller.Length; ++n) {
        name[n] = (string)nameModel[2 * n];
        controller[n] = new Controller((IModel<string>)nameModel[2 * n + 1], bg);
      }
    }
    /// <summary> connect controllers and delegate event handlers. </summary>
    /// <param name="enable"> controls user interaction in view. </param>
    /// <param name="io"> current/size/search/enter/remove fields. </param>
    /// <param name="click"> add to eventhandlers, null to skip. </param>
    public virtual void Connect (Enable enable, IAccess[] io, SetClick[] click) {
      // connect controllers for i/o
      for (int n = controller.Length; --n >= 0; )
        controller[n].Connect(enable, io);
      // show current name and size
      io[0].Text = name[current];
      controller[current].doSize(null, null);
      // delegate event handlers
      if (click[0] != null) click[0]((sender, e) => {
        // advance controller, show name and size
        current = (current + 1) % controller.Length;
        io[0].Text = name[current];
        controller[current].doSize(null, null);
      });
      if (click[1] != null) click[1]((sender, e) => controller[current].doSearch(sender, e));
      if (click[2] != null) click[2]((sender, e) => controller[current].doEnter(sender, e));
      if (click[3] != null) click[3]((sender, e) => controller[current].doRemove(sender, e));
      if (click[4] != null) click[4]((sender, e) => controller[current].doSize(null, null));
    }
    /// <summary> access the current index. </summary>
    public int Current {
        get { return current; }
        set { current = value; }
    }
  }
}