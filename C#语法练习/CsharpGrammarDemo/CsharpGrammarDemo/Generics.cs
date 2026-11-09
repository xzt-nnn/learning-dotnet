using System;
using System.Collections.Generic;

namespace CsharpGrammarDemo
{
    // 3.1 基类约束：T 必须是 Animal 或 Animal 的子类
    public abstract class Animal
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Dog : Animal
    {
    }

    public class AnimalBox<T> where T : Animal
    {
        public T? Item { get; set; }

        public void ShowName()
        {
            if (Item != null)
            {
                Console.WriteLine(Item.Name);
            }
        }
    }

    // 3.2 new() 约束：T 必须有无参构造，编译器才允许 new T()
    public class Factory<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T();
        }
    }

    // 3.3 class 约束：T 只能是引用类型，不能是 int、double 等值类型
    public class RefHelper<T> where T : class
    {
        public T? Data { get; set; }
    }

    // 3.5 接口约束：T 必须实现 IPlayable
    public interface IPlayable
    {
        void Play();
    }

    public class Song : IPlayable
    {
        public void Play()
        {
            Console.WriteLine("播放歌曲");
        }
    }

    public class Player<T> where T : IPlayable
    {
        public void Start(T item)
        {
            item.Play();
        }
    }

    // 3.4 多约束组合：引用类型 + 无参构造函数
    public class Cache<T> where T : class, new()
    {
        private readonly List<T> items = new List<T>();

        public T Get()
        {
            T item = new T();
            items.Add(item);
            return item;
        }
    }

    internal class Generics
    {
        static void Main(string[] args)
        {
            // 3.1 基类约束
            AnimalBox<Dog> box = new AnimalBox<Dog> { Item = new Dog { Name = "旺财" } };
            box.ShowName();

            // 3.2 new() 约束
            Factory<Dog> factory = new Factory<Dog>();
            Dog dog = factory.CreateInstance();
            Console.WriteLine(dog.GetType().Name);

            // 3.3 class 约束
            RefHelper<string> helper = new RefHelper<string> { Data = "hello" };
            Console.WriteLine(helper.Data);

            // 3.5 接口约束
            Player<Song> player = new Player<Song>();
            player.Start(new Song());

            // 3.4 多约束组合
            Cache<Song> cache = new Cache<Song>();
            cache.Get();
        }
    }
}
