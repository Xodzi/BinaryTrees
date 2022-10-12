# BinaryTrees (Бинарные деревья)
Это **учебная** библиотека, которая реализует бинарные и балансированные АВЛ-деревья на языке C#.
В первую очередь библиотека была для меня способом лучше погрузится в данную структуру данных.
Думаю, что она способна помочь тем кто только начал изучать деревья в программировании.

---

## Бинарные деревья

В начале рассмотрим реализацию простых бинарных деревьев.

### Бинарная вершина

Содержит три поля:
+ Значение узла
+ Ссылка на левый подузел
+ Ссылка на правый подузел
В вызываемый конструктор можно передать только значение
``` C#
public BinNode(T value, BinNode<T> left = null, BinNode<T> right= null)
        { Value = value; Left = left; Right = right; }
```

## Реализация дерева

Реализация дерева достаточно простая. Все методы описаны в самих классах при помощи тега summary.
Реализация добавления новых вершин.
```C#
public void Add(ref BinNode<T> node, T value)
        {
            if (node == null) node = new BinNode<T>(value);
            else
            {
                if (node.Value.CompareTo(value) > 0)
                    Add(ref node.Left, value); // node.Value > value
                else Add(ref node.Right, value); // node.Value <= value
                                                 //if (node.Value.CompareTo(value) < 0) // уник. дерево
                                                 // Add(ref node.Right, value); // node.Value < value
            }
        }
```

Добавлять можно и массивы данных.
```C#
public BinTree(T[] items) { foreach (T item in items) Add(item); }
```
____
## АВЛ-деревья
