using System;
using System.Runtime.CompilerServices;

namespace GameFramework
{
	/// <summary>
	/// A lock free Ring buffer.
	/// </summary>
	/// <exception cref='IndexOutOfRangeException'>
	/// Is thrown when an attempt is made to access an element of an array with an index that is outside the bounds of the array.
	/// </exception>
	public class RingBuffer<T>
	{
		private int size;
		private int read = 0;
		private int write = 0;
		private int count = 0;
		private T[] objects;


        public int AvailableToRead {
            get { 
                return count;
            }
        }

        public int AvailableToWrite {
            get { 
                return size - count;
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="RingBuffer`1"/> class with a maximum capacity.
		/// </summary>
		/// <param name='size'>
		/// Size.
		/// </param>
		public RingBuffer (int size)
		{
			this.size = size;
			objects = new T[size + 1];
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="RingBuffer`1"/> is empty.
		/// </summary>
		/// <value>
		/// <c>true</c> if empty; otherwise, <c>false</c>.
		/// </value>
		public bool Empty {
			get { return (read == write) && (count == 0); }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="RingBuffer`1"/> is full.
		/// </summary>
		/// <value>
		/// <c>true</c> if full; otherwise, <c>false</c>.
		/// </value>
		public bool Full {
			get { return (read == write) && (count > 0); }
		}

		/// <summary>
		/// Write the specified src array into the buffer.
		/// </summary>
		/// <param name='src'>
		/// Source.
		/// </param>
		/// <exception cref='IndexOutOfRangeException'>
		/// Is thrown when no room is left for the array.
		/// </exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Write (T[] src)
		{
			if (Full)
				throw new IndexOutOfRangeException ("Queue Full!");
			if (size - count < src.Length)
				throw new IndexOutOfRangeException ("Queue Almost Full!");
            
			if (write > read && size - write < src.Length) {
				var b = size - write;
				Array.Copy (src, 0, objects, write, b);
				count += b;
				write = (write + b) % size;
				var r = src.Length - b;
				Array.Copy (src, b, objects, write, r);
				count += r;
				write = (write + r) % size;
			} else {
				Array.Copy (src, 0, objects, write, src.Length);
				count += src.Length;
				write = (write + src.Length) % size;
			}
		}

		/// <summary>
		/// Write a single item into the buffer.
		/// </summary>
		/// <param name='item'>
		/// Item.
		/// </param>
		/// <exception cref='IndexOutOfRangeException'>
		/// Is thrown when there is no room in the buffer.
		/// </exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Write (T item)
		{

            if (Full)
                throw new IndexOutOfRangeException ("Queue Full!");
        
			objects [write] = item;
			count++;
			write = (write + 1) % size;
		}

		/// <summary>
		/// Read from the buffer into the dest array.
		/// </summary>
		/// <param name='dest'>
		/// Destination.
		/// </param>
		/// <exception cref='IndexOutOfRangeException'>
		/// Is thrown when an attempt is made to read more data than is available
		/// </exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Read (T[] dest)
		{
			if (Empty)
				throw new IndexOutOfRangeException ("Queue Empty!");
			if (count < dest.Length)
				throw new IndexOutOfRangeException ("Queue Almost Empty!");
            
			if (size - read >= dest.Length) {
				Array.Copy (objects, read, dest, 0, dest.Length);
				count -= dest.Length;
				read = (read + dest.Length) % size;
			} else {
				var b = size - read;
				Array.Copy (objects, read, dest, 0, b);
				count -= b;
				read = (read + b) % size;
				var r = dest.Length - b;
				Array.Copy (objects, read, dest, b, r);
				count -= r;
				read = (read + r) % size;
			}
		}

		/// <summary>
		/// Read a single item from the buffer.
		/// </summary>
		/// <exception cref='IndexOutOfRangeException'>
		/// Is thrown when an attempt is made to read more data than is available
		/// </exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public T Read ()
		{
            if (Empty)
                throw new IndexOutOfRangeException ("Queue Empty!");

			T item = objects [read];
			count--;
			read = (read + 1) % size;
			return item;
		}
	}
}