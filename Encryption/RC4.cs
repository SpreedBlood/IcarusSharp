namespace IcarusSharp.Encryption
{
    public class RC4
    {
        private static readonly int POOL_SIZE = 256;

        private int i = 0;
        private int j = 0;
        private int[] table;

        public RC4()
        {
            table = new int[POOL_SIZE];
        }

        public RC4(byte[] key)
        {
            table = new int[POOL_SIZE];
            Init(key);
        }

        public void Init(byte[] key)
        {
            i = 0;
            j = 0;

            for (i = 0; i < POOL_SIZE; ++i)
            {
                table[i] = (byte)i;
            }

            for (i = 0; i < POOL_SIZE; ++i)
            {
                j = (j + table[i] + key[i % key.Length]) & (POOL_SIZE - 1);
                Swap(i, j);
            }

            i = 0;
            j = 0;
        }

        public void Swap(int a, int b)
        {
            int k = table[a];
            table[a] = table[b];
            table[b] = k;
        }

        public byte Next()
        {
            i = ++i & (POOL_SIZE - 1);
            j = (j + table[i]) & (POOL_SIZE - 1);
            Swap(i, j);

            return (byte)table[(table[i] + table[j]) & (POOL_SIZE - 1)];
        }

        public byte[] Decipher(byte[] array)
        {

            byte[] result = new byte[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = (byte)(array[i] ^ Next());
            }

            return result;
        }
    }
}