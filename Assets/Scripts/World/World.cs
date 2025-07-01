using UnityEngine;


namespace UnityMC {

    [ExecuteInEditMode]
    public class World : MonoBehaviour
    {

        public int width;
        public int height;
        public int depth;
        public int seed;
        [Range(0, 1)] public float treeProbability;
        public Blocks blocks;

        public static World instance;

    
        private void Awake() 
        {
            instance = this;
            //Generate();
        }
        
        [ContextMenu("生成地形")]
        public void Generate() 
        {
            Clean();

            Random.InitState(seed);


            for (float x = -(width/2); x < width / 2; x++) {

                for (float z = -(depth/2); z < depth / 2; z++) {

                    int y = (int)(Mathf.PerlinNoise((x / 2 + seed) / 10, (z / 2 + seed) / 10) * 10);

                    BaseBlock block = Instantiate(blocks.grass, new Vector3(x, y, z), Quaternion.identity);

                    block.transform.SetParent(transform);


                    Vector3 grassPosition = new Vector3(x, y, z) - new Vector3(0, 2, 0);

                    CreateBlockLine(new Vector3(x, y, z), grassPosition, blocks.dirt);


                    Vector3 stonePosition = new Vector3(x, -height + 1, z);

                    CreateBlockLine(grassPosition, stonePosition, blocks.stone);


                    Vector3 bedrockPosition = new Vector3(x, -height, z);

                    CreateBlockLine(stonePosition, bedrockPosition, blocks.bedrock);


                    Vector3 treePosition = new Vector3(x, y, z);

                    if (Random.Range(0, 1f) < treeProbability) { CreateTree(treePosition); }

                }

            }

        }

        public void CreateBlockLine(Vector3 from, Vector3 to, BaseBlock prefab) {

            Vector3 position = from;

            do {

                if (position.y < to.y) { position.y++; }

                if (position.y > to.y) { position.y--; }

                BaseBlock block = Instantiate(prefab, new Vector3(position.x, position.y, position.z), Quaternion.identity);

                block.transform.SetParent(transform);

            } while (position != to);

        }

        public void CreateTree(Vector3 position) {

            CreateBlockLine(position, position + Vector3.up * 4, blocks.log);

            // Leaves

            Vector3[] relativePositions = new Vector3[] { new Vector3(1, 2, 0), new Vector3(1, 3, 0), 
                new Vector3(1, 4, 0), new Vector3(-1, 2, 0), new Vector3(-1, 3, 0), new Vector3(-1, 4, 0),
                new Vector3(0, 2, 1), new Vector3(0, 3, 1), new Vector3(0, 4, 1), new Vector3(0, 2, -1),
                new Vector3(0, 3, -1), new Vector3(0, 4, -1), new Vector3(0, 4, 0), new Vector3(1, 2, 1),
                new Vector3(1, 3, 1), new Vector3(1, 2, -1), new Vector3(1, 3, -1), new Vector3(-1, 2, -1),
                new Vector3(-1, 3, -1), new Vector3(-1, 2, 1), new Vector3(-1, 3, 1) };

            foreach (Vector3 relativePosition in relativePositions) {

                Instantiate(blocks.leaves, position + Vector3.up + relativePosition, Quaternion.identity, parent: transform);

            }

        }

        public void Clean() {

            while (transform.childCount > 0) {

                DestroyImmediate(transform.GetChild(0).gameObject);

            }

        }


        [System.Serializable]
        public struct Blocks {

            public BaseBlock grass;
            public BaseBlock dirt;
            public BaseBlock stone;
            public BaseBlock bedrock;
            public BaseBlock log;
            public BaseBlock leaves;

        }

    }

}