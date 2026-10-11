public interface ISkillBase
{
	bool CanExecute();
	void Begin();
	void Tick();
	void End();
	void Cancel();
	bool IsFinished { get; }
	bool IsRunning { get; }
}
