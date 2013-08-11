using System;
using EventStore.Core.Bus;
using EventStore.Projections.Core.Messages;

namespace EventStore.Projections.Core.Services.Processing
{

    public enum PhaseState
    {
        Unknown,
        Stopped,
        Running
    }


    public interface IProjectionProcessingPhase : IDisposable, IHandle<EventReaderSubscriptionMessage.CommittedEventReceived>, 
        IHandle<EventReaderSubscriptionMessage.ProgressChanged>,
        IHandle<EventReaderSubscriptionMessage.NotAuthorized>,
        IHandle<EventReaderSubscriptionMessage.EofReached>,
        IHandle<EventReaderSubscriptionMessage.CheckpointSuggested>,
        IHandle<CoreProjectionManagementMessage.GetState>,
        IHandle<CoreProjectionManagementMessage.GetResult>,
        IHandle<CoreProjectionProcessingMessage.PrerecordedEventsLoaded>
    {
        void InitializeFromCheckpoint(CheckpointTag checkpointTag);

        void ProcessEvent();

        //TODO: remove from - it is passed for validation purpose only
        void Subscribe(CheckpointTag from, bool fromCheckpoint);

        void SetProjectionState(PhaseState state);

        void GetStatistics(ProjectionStatistics info);
        CheckpointTag MakeZeroCheckpointTag();
        ICoreProjectionCheckpointManager CheckpointManager { get; }

        void EnsureUnsubscribed();
    }
}