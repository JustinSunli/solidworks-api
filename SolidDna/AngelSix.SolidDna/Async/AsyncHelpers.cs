﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace AngelSix.SolidDna
{
    /// <summary>
    /// Helper methods for working with async
    /// </summary>
    public static class AsyncHelpers
    {
        #region Private Members

        private static readonly TaskFactory mTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        #endregion

        /// <summary>
        /// Helper to run an asynchronous function as synchronous where all internal await's are decorated with .ConfigureAwait(false)
        /// </summary>
        /// <typeparam name="TResult">The type of return data the function returns</typeparam>
        /// <param name="func">The function to run</param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return mTaskFactory
              .StartNew<Task<TResult>>(func)
              .Unwrap<TResult>()
              .GetAwaiter()
              .GetResult();
        }

        /// <summary>
        /// Helper to run an asynchronous function as synchronous where all internal await's are decorated with .ConfigureAwait(false)
        /// </summary>
        /// <param name="func">The function to run</param>
        /// <returns></returns>
        public static void RunSync(Func<Task> func)
        {
            mTaskFactory
              .StartNew<Task>(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }
    }
}
