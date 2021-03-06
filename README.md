Структуры данных, которые уже присутсвуют в библиотеке классов, отлично решают задачи заявленные перед ними. Но что делать, если понимаешь что придётся столкнуться с задачей, которая решается очередью с приоритетом? Здесь уже нет готового SDK, и придётся либо тянуть чужую реализацию (или целую библиотеку) себе в проект, либо всё писать самому на коленке. 
Давайте сравним хотя-бы в первом приближении на базе какой структуры данных оптимальнее, в плане скорости работы и используемой памяти, реализовать очередь с приоритетом. Попробуем понять, что нам больше подойдёт двусвязный список (LinkedList<T>), массив, список (List<T>) и SortedSet. В большинстве случаев список, будет универсальной структурой данных. Но так ли это будет и при решении нашей проблемы?


<!--more-->
## Очередь с приоритетом

Конечно все знают, но я напомню, что очередь с приоритетом это такая структура данных, которая описывается интерфейсом взаимодействия очереди, но то когда элемент будет извлечён из очереди (Dequeue/Pop) определяется некоторым приоритетом элемента. 
Предлагаю интерфейс описать следующим интерфейсом: 

```csharp 
public interface IPriorityQueue<T> 
{
	void Enqueue(T item);
	T Dequeue();
}
```

Как видим этого мало - приоритет мы так и не задали. Предлагаю приоритет определять при помомщи IComparer и передавать его в очередь при создании. Т.к. интерфейс на конструктора мы дотнетчики не имеем, так что `"Ладно! И так сойдёт!!!".jpg`. Давайте же не будем мудрствовать и переиспользуем очередь из какого нибудь авторитетоного источника. Думаю Robert Sedgewick это самый надёжный вариант. 

### Массив

Конструктор будет принимать компарер и размер массива. Пожалуй это самая большая печаль - размер кучи мы должны или менять по ходу копируя или оставить статическим. Ну пусть будет статичесский, вариант с копированием мы всё равно не сможем сделать лучше чем в системном списке (List). Что же на счёт реализации? 

Думаю лучшим вариантом изучить работу этой очереди будет всё-таки избежать этого, сославшись на [книгу](http://letmegooglethat.com/?q=%D0%BA%D0%BD%D0%B8%D0%B3%D0%B0+Robert+Sedgewick) или [курс](https://www.coursera.org/learn/algorithms-part1) с участием Седжвика. Я, конечнo, дам короткое пояснение работы, но это будет далеко не так познавательно. 

{{< mermaid align="left">}}
graph TD
node1(1-й элемент);
node1 --> node2(2-й элемент);
node1 --> node3(3-й элемент);
node2 --> node4(4-й элемент);
node2 --> node5(5-й элемент);
node3 --> node6(6-й элемент);
node3 --> node7(7-й элемент);
node4 --> node8(8-й элемент);
node4 --> node9(9-й элемент);

classDef className fill:#f9f,stroke:#333,stroke-width:4px;
class node1,node3,node7 className;
{{< /mermaid >}}

Куча определяется как бинарное дерево в виде массива с информацией, о последнем свободном элементе. Бинарное дерево будет опредеять "приоритетность" близостью к вершине. т.е. родительский элемент всегдя будет выше приоритетом чем дочерний. 
Для простоты арифметики индексации давайте считать первой ячейкой не ячейку массива с индексом 0 а с индексом 1.
Смотрите как это работает: у элемента с индексом 7м есть как минимум две элемента с бОльшим приоритетом - две уровня родителей. Что легко можно расчитать при помощи деления на два. Это третий элемент 3 и 1 (3 = 7 / 2; 1 = 3 / 2). Возможно больше, но пока мы не сравним дочерние элементы у одного родителя мы точно этого не знаем.
Стоит отметить, что две операции добавления и удаления потребуют сложности в O(lg N). Как этого добится?
При добавлении мы ставим элемент последним в массиве и пытаемся его "поднять", сравнивая со всеми родительскими по очереди.
```csharp
private void MoveUp() 
{
	var i = indx;
	while (i != 1 && this.comparer.Compare(this.heap[i / 2], this.heap[i]) > 0) 
	{
		Swap(this.heap, i / 2, i);
		i /= 2;
	}
}
```

При удалении мы берём первый элемент из кучи и заменяем его последним. После чего новый первый элемент "опускаем", попутно выбирая из двух дочерних элементов элемент с более высоким приоритетом для замены. Как говорится лучше один раз увидеть, чем сто услышать:


```csharp
private void MoveDown()
{
	var i = 1;
	while (i * 2 <= this.indx)
	{
		var next = i * 2;
		if (next < this.indx && this.comparer.Compare(this.heap[next], this.heap[next + 1]) > 0) next++;
		if (this.comparer.Compare(this.heap[next], this.heap[i]) > 0) break;
		Swap(this.heap, next, i);
		i = next;
	}
}
```
Настойчиво рекомендую просмотреть бесплатные курсы от [Kevin Wayne and Robert Sedgewick](https://www.coursera.org/learn/algorithms-part1).

### Список

С одной стороны мы мжем не передавать в конструктор размер кучи. А с другой - начинается колхоз и самострой. Но я бы не сказал что его тут много. Операции с доступом к последнему элементу мы перекладываем на список и просто добавляем и удаляем элементы. Фактически доступ данным через индексаторы у нас есть. Можете ознакомтися с рализацией в моём репозитории (TBD).

### Связанный список

Вот тут всё не айс. Для доступа к элементам нужно добавлять счётчики и считать разницу. Это, конечно, добавляет сложности, но оставляет возможным и такую реализацию. К примеру вот так выглядит тот же код для двусвязного списка, что я привёл выше для массива:

```csharp
private void MoveDown()
{
	var indx = 1;
	var prev = this.heap.First;
	while (prev != null && indx * 2 <= this.heap.Count)
	{
		var next = ScrollForward(prev, indx);
		indx *= 2;
		if (indx < this.heap.Count && this.comparer.Compare(next.Value, next.Next.Value) > 0)
		{
			indx++;
			next = ScrollForward(next, 1);
		}
		if (this.comparer.Compare(next.Value, prev.Value) > 0) break;
		Swap(prev, next);
		prev = next;
	}
}
```
Не выглядит как что-то что работает быстро, из-за постоянных обращений в память и невозможностью быть закешированным поцессором. Но возможно, это окажется быстрее при дальнейшей сборке мусора.

### SortedSet

Вариант для самых ленивых. Когда "делай проще" не только девиз по жизни, но и в KISS принципе, но и девиз по жизни. Это реализация красно-чёрного сортированного дерева из библиотеки классов платформы. На самом деле выглядит круто. 

## Дизайн испытания

Испытания будем проводить на базе популярного фреймворка [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) идеально подходящего и для наших целей.
Для тестов я подготовил вот такую заготовочку:
1. генерируем N случайных чисел
2. в методе условиях бенчмарка создаём очередь, после чего добавляем эти числа и удаялем их.
Таким образом мы будем учитывать время на аллокацию и освобождение памяти.

```csharp
[Benchmark]
public void Array()
{
	var pq = new ArrayPriorityQueue(MaxComparer, Size);
	foreach (var item in randomItems)
	{
		pq.Enqueue(item);
	}

	for (int i = 0; i < randomItems.Count; i++)
	{
		pq.Dequeue();
	}
}
```

## Результаты испытаний

```
// * Summary *

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
[Host]     : .NET Core 2.1.16 (CoreCLR 4.6.28516.03, CoreFX 4.6.28516.10), X64 RyuJIT
DefaultJob : .NET Core 2.1.16 (CoreCLR 4.6.28516.03, CoreFX 4.6.28516.10), X64 RyuJIT


|     Method |     Mean |   Error |  StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|----------- |---------:|--------:|--------:|-------:|-------:|------:|----------:|
|      Array | 110.0 us | 1.95 us | 1.82 us |      - |      - |     - |         - |
|       List | 168.9 us | 3.45 us | 9.90 us |      - |      - |     - |         - |
|  SortedSet | 245.3 us | 4.75 us | 4.66 us | 6.3477 | 0.2441 |     - |   40000 B |
| LinkedList | 322.5 us | 5.75 us | 6.39 us | 7.3242 | 0.4883 |     - |   48000 B |
```

Как я и предполал, самым быстрым решением будет, решение на очереди. В полтора раза медленнее оказалось решение на базе списка. Это скорее всего вызвано дополнительными аллокациями памяти. 
