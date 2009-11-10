using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace ATS.Database {
  /// <summary> a queue of pairs of jobs to be run
  ///   alternating a background and the event thread. </summary>
  public class WorkQueue {
    /// <summary> pairs of jobs to be done in background/event thread. </summary>
    protected readonly Queue<ThreadStart> toDo = new Queue<ThreadStart>();
    /// <summary> handles a burst of job pairs. </summary>
    protected BackgroundWorker worker;
    /// <summary> add jobs to the queue. </summary>
    /// <param name="jobs"> should be one or more pairs. </param>
    public virtual void Enqueue (ThreadStart[] jobs) {
      lock (toDo) {
        foreach (var job in jobs) toDo.Enqueue(job);
        if (worker == null) Run();
      }
    } 
    /// <summary> execute jobs currently in the queue </summary>
    public virtual void Run () {
      // one burst of jobs
      worker = new BackgroundWorker();
      // to be done in event thread
      Queue<ThreadStart> toFg = new Queue<ThreadStart>();
      worker.DoWork += (DoWorkEventHandler) delegate {
        // run all bg parts in queue, save fg parts
        while (true) {
          ThreadStart bg;
          lock(toDo) {
            if (toDo.Count < 2) { // no more pairs
              worker = null;
              break;
            }
            bg = toDo.Dequeue(); toFg.Enqueue(toDo.Dequeue());
          }
          bg();
        }
      };
      // now run all fg parts
      worker.RunWorkerCompleted += (RunWorkerCompletedEventHandler) delegate {
        while (toFg.Count > 0) toFg.Dequeue()();
      };
      worker.RunWorkerAsync();
    }
  }
 }
