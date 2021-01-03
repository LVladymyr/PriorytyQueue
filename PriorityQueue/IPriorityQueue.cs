namespace PriorityQueue 
{
	public interface IPriorityQueue<T> 
	{
		void Enqueue(T item);
		T Dequeue();
	} 
} 
