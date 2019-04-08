using System.Collections;
using System.Collections.Generic;
using System;

public class Heap <T> where T : IHeapItem<T>{

    T[] itemsArray;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        itemsArray = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        itemsArray[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T RemoveFirstItem()
    {
        T firstItem = itemsArray[0];
        currentItemCount--;
        itemsArray[0] = itemsArray[currentItemCount];
        itemsArray[0].HeapIndex = 0;
        SortDown(itemsArray[0]);
        return firstItem;
    }

    public bool Contains(T item)
    {
        return Equals(itemsArray[item.HeapIndex], item);
    }

    public int Count
    {
        get { return currentItemCount; }
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public void SortDown(T item)
    {
        int leftChildIndex;
        int rightChildIndex;
        int swapIndex;
        while (true)
        {
            leftChildIndex = item.HeapIndex * 2 + 1;
            rightChildIndex = item.HeapIndex * 2 + 2;

            if(leftChildIndex < currentItemCount)
            {
                swapIndex = leftChildIndex;
                if (rightChildIndex < currentItemCount)
                {
                    if (itemsArray[leftChildIndex].CompareTo(itemsArray[rightChildIndex]) < 0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }
                
                if(item.CompareTo(itemsArray[swapIndex]) < 0)
                {
                    Swap(item, itemsArray[swapIndex]);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    public void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while(true)
        {
            T parentItem = itemsArray[parentIndex];
            if(item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    public void Swap(T item1, T item2)
    {
        itemsArray[item1.HeapIndex] = item2;
        itemsArray[item2.HeapIndex] = item1;

        int item1Index = item1.HeapIndex;
        item1.HeapIndex = item2.HeapIndex;
        item2.HeapIndex = item1Index;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
