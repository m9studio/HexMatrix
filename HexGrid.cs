using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace M9Studio.HexMatrix
{
    public class HexGrid<T> : IEnumerable<(HexGridPosition, T)>, IReadOnlyDictionary<HexGridPosition, T>
    {
        private T center;
        private T[,] A;
        private T[,] B;
        private T[,] C;
        private int radius;

        public int Count => 1 + A.Length + B.Length + C.Length;
        public int Radius => radius;

        public T this[int x, int y, int z]
        {
            get => Get(x, y, z);
            set => Set(x, y, z, value);
        }
        public T this[HexGridPosition position]
        {
            get => Get(position);
            set => Set(position, value);
        }





        public HexGrid(int radius)
        {
            this.radius = radius;
            if (radius > 1)
            {
                A = new T[radius, radius + 1];
                B = new T[radius, radius + 1];
                C = new T[radius, radius + 1];
            }
        }


        public void Set(HexGridPosition position, T value) => Set(position.X, position.Y, position.Z, value);
        public void Set(int x, int y, int z, T value)
        {
            if (x + y + z != 0)
                throw new ArgumentException("Invalid cube coordinates: x + y + z must equal 0.");

            if (x == 0 && y == 0 && z == 0)
            {
                center = value;
                return;
            }
            if (x >= 1 && y >= 0 && y <= radius)
            {
                A[x - 1, y] = value; // XY + ось X
                return;
            }
            if (y >= 1 && z >= 0 && z <= radius)
            {
                B[y - 1, z] = value; // YZ + ось Y
                return;
            }
            if (x >= 1 && z >= 0 && z <= radius)
            {
                C[x - 1, z] = value; // XZ + ось Z
                return;
            }
            throw new ArgumentOutOfRangeException("x/y/z", $"Coordinates ({x},{y},{z}) are out of bounds.");
        }
        public T Get(HexGridPosition position) => Get(position.X, position.Y, position.Z);
        public T Get(int x, int y, int z)
        {
            if (x + y + z != 0)
                throw new ArgumentException("Invalid cube coordinates: x + y + z must equal 0.");

            if (x == 0 && y == 0 && z == 0)
                return center;

            if (x >= 1 && y >= 0 && y <= radius)
                return A[x - 1, y]; // XY + ось X
            if (y >= 1 && z >= 0 && z <= radius)
                return B[y - 1, z]; // YZ + ось Y
            if (x >= 1 && z >= 0 && z <= radius)
                return C[x - 1, z]; // XZ + ось Z

            throw new ArgumentOutOfRangeException("x/y/z", $"Coordinates ({x},{y},{z}) are out of bounds.");
        }

        public void Fill(T value)
        {
            center = value;
            for (int i = 0; i < radius; i++)
            {
                for (int j = 0; j < radius + 1; j++)
                {
                    A[i, j] = value;
                    B[i, j] = value;
                    C[i, j] = value;
                }
            }
        }





        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public IEnumerator<(HexGridPosition, T)> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<HexGridPosition> Keys => throw new NotImplementedException();
        public IEnumerable<T> Values => throw new NotImplementedException();
        public bool ContainsKey(HexGridPosition key) => key.X > Radius || key.Y > Radius || key.Z > Radius || key.X < -Radius || key.Y < -Radius || key.Z < -Radius;

        public bool TryGetValue(HexGridPosition key, [MaybeNullWhen(false)] out T value)
        {
            try
            {
                value = Get(key);
                return true;
            }
            catch (Exception ex)
            {
                value = default!;
                return false;
            }
        }
        IEnumerator<KeyValuePair<HexGridPosition, T>> IEnumerable<KeyValuePair<HexGridPosition, T>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }




    }
}
