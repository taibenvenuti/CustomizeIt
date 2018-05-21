using UnityEngine;

namespace CustomizeIt.AI
{
    public class RPCData
    {
        public const int PEOPLE = 0; 
        public const int LEVEL_HEIGHT = 1;
        public const int DENSIFICATION = 2;
        public const int CALC_METHOD = 3;
        public const int VISIT = 4;
        public const int WORK_LVL0 = 5;
        public const int WORK_LVL1 = 6;
        public const int WORK_LVL2 = 7;
        public const int WORK_LVL3 = 8;
        public const int POWER = 9;
        public const int WATER = POWER + 1;
        public const int SEWAGE = WATER + 1;
        public const int GARBAGE = SEWAGE + 1;
        public const int INCOME = GARBAGE + 1;
        public const int GROUND_POLLUTION = 14;
        public const int NOISE_POLLUTION = 15;
        public const int PRODUCTION = 16;

        public static int CalculatePrefabHousehold(BuildingInfo building, int[] array)
        {
            Vector3 size = building.m_generatedInfo.m_size;
            int floorCount = Mathf.Max(1, Mathf.FloorToInt(size.y / array[LEVEL_HEIGHT]));
            var width = building.m_cellWidth;
            var length = building.m_cellLength;
            int returnValue = (CalcBase(width, length, ref array, size) * floorCount) / array[PEOPLE];

            if ((building.m_class.m_subService == ItemClass.SubService.ResidentialHigh) || (building.m_class.m_subService == ItemClass.SubService.ResidentialHighEco))
            {
                returnValue = Mathf.Max(Mathf.Max(2, Mathf.CeilToInt(0.9f * floorCount)), returnValue);
            }
            else
            {
                returnValue = Mathf.Max(1, returnValue);
            }

            return returnValue;
        }

        public static void CalculateprefabWorkerVisit(BuildingInfo building, int[] array, out int out0, out int out1, out int out2, out int out3, out int visitors)
        {
            int value = 0;
            int num = array[PEOPLE];
            int level0 = array[WORK_LVL0];
            int level1 = array[WORK_LVL1];
            int level2 = array[WORK_LVL2];
            int level3 = array[WORK_LVL3];
            int num2 = level0 + level1 + level2 + level3;
            var width = building.m_cellWidth;
            var length = building.m_cellLength;

            if (num > 0 && num2 > 0)
            {
                Vector3 v = building.m_generatedInfo.m_size;
                int floorSpace = CalcBase(width, length, ref array, v);
                int floorCount = Mathf.Max(1, Mathf.FloorToInt(v.y / array[LEVEL_HEIGHT])) + array[DENSIFICATION];
                value = (floorSpace * floorCount) / array[PEOPLE];

                num = Mathf.Max(0, value);

                out3 = (num * level3) / num2;
                out2 = (num * level2) / num2;
                out1 = (num * level1) / num2;
                out0 = Mathf.Max(0, num - out3 - out2 - out1);
            }
            else
            {
                out0 = out1 = out2 = out3 = 1;
            }
            
            if (num != 0)
            {
                value = Mathf.Max(200, width * length * array[VISIT]) / 100;
            }
            visitors = value;
        }

        private static int CalcBase(int width, int length, ref int[] array, Vector3 v)
        {
            if (array[CALC_METHOD] == 0)
            {
                if (v.x <= 1)
                {
                    width *= 6;
                }
                else
                {
                    width = (int)v.x;
                }

                if (v.z <= 1)
                {
                    length *= 6;
                }
                else
                {
                    length = (int)v.z;
                }
            }
            else
            {
                width *= 64;
            }

            return width * length;
        }

        public static int[][] residentialLow = new int[][] 
        {
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    8, 20, 15, 11, 130,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    8, 21, 16, 10, 140,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    9, 22, 17, 10, 150,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    9, 24, 19,  9, 160,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,   10, 26, 21,  9, 170,   0, 1,   -1 }
        };

        public static int[][] residentialHigh = new int[][]
        {
            new int [] { 140, 5, -1, 0, -1,   -1, -1, -1, -1,    7, 14, 11, 9,  98,   0, 5,   -1 },
            new int [] { 145, 5, -1, 0, -1,   -1, -1, -1, -1,    7, 15, 12, 8, 105,   0, 5,   -1 },
            new int [] { 150, 5, -1, 0, -1,   -1, -1, -1, -1,    8, 16, 13, 8, 113,   0, 5,   -1 },
            new int [] { 160, 5, -1, 0, -1,   -1, -1, -1, -1,    8, 17, 14, 7, 120,   0, 5,   -1 },
            new int [] { 170, 5, -1, 0, -1,   -1, -1, -1, -1,    9, 19, 16, 7, 127,   0, 5,   -1 }
        };

        public static int[][] residentialEcoLow = new int[][]
        {
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    6, 19, 15, 8,  98,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    6, 21, 17, 8, 105,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    7, 23, 19, 7, 112,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    8, 25, 21, 6, 119,   0, 1,   -1 },
            new int [] { 2000, 50, -1, 0, -1,   -1, -1, -1, -1,    8, 28, 24, 6, 126,   0, 1,   -1 }
        };

        public static int[][] residentialEcoHigh = new int[][]
        {
            new int [] { 150, 5, -1, 0, -1,   -1, -1, -1, -1,    6, 14, 12, 7, 74,   0, 3,   -1 },
            new int [] { 155, 5, -1, 0, -1,   -1, -1, -1, -1,    6, 16, 14, 6, 78,   0, 3,   -1 },
            new int [] { 160, 5, -1, 0, -1,   -1, -1, -1, -1,    6, 18, 16, 6, 84,   0, 3,   -1 },
            new int [] { 165, 5, -1, 0, -1,   -1, -1, -1, -1,    7, 20, 18, 5, 89,   0, 3,   -1 },
            new int [] { 170, 5, -1, 0, -1,   -1, -1, -1, -1,    8, 22, 20, 5, 95,   0, 3,   -1 }
        };

        public static int[][] commercialLow = new int[][]
        {
            new int [] { 100, 6, 1, 0,  90,   70, 20, 10,  0,    9, 30, 30, 9, 800,   0, 100,   -1 },
            new int [] { 105, 6, 1, 0, 100,   30, 45, 20,  5,   10, 35, 35, 8, 850,   0,  90,   -1 },
            new int [] { 110, 6, 1, 0, 110,    5, 30, 55, 10,   11, 40, 40, 7, 900,   0,  75,   -1 }
        };

        public static int[][] commercialHigh = new int[][]
        {
            new int [] { 115, 5, 1, 0, 220,   10, 45, 40,  5,   10, 28, 28, 9, 800,   0, 80,   -1 },
            new int [] { 120, 5, 1, 0, 310,    7, 32, 43, 18,   11, 32, 32, 8, 850,   0, 70,   -1 },
            new int [] { 125, 5, 1, 0, 400,    5, 25, 45, 25,   13, 36, 36, 7, 900,   0, 60,   -1 }
        };

        public static int[][] commercialEcoLow = new int[][] 
        {
            new int[] { 120, 6, 1, 0, 100,    50, 40, 10, 0,    11, 30, 30, 7, 800,    0, 2,    50 }
        };

        public static int[][] commercialTourist = new int[][] 
        {
            new int[] { 1000, 10, 50, 0, 250,    15, 35, 35, 15,   30, 50, 55, 30, 900,    0, 150,   -1 }
        };

        public static int[][] commercialLeisure = new int[][] 
        {
            new int[] { 60, 10, 0, 0, 250,    15, 40, 35, 10,    30, 36, 40, 25, 750,    0, 300,   -1 }
        };

        public static int[][] office = new int[][]
        {
            new int [] { 34, 5, 0, 0, -1,   2,  8, 20, 70,   12, 4, 4, 3, 1100,   0, 1,   10 },
            new int [] { 36, 5, 0, 0, -1,   1,  5, 14, 80,   13, 5, 5, 3, 1175,   0, 1,   10 },
            new int [] { 38, 5, 0, 0, -1,   1,  3,  6, 90,   14, 5, 5, 2, 1250,   0, 1,   10 }
        };

        public static int[][] officeHighTech = new int[][] 
        {
            new int[] { 74, 5, 0, 0, -1,   1, 2, 3, 94,   22, 5, 5, 3, 4000,   0, 1,   10 }
        };

        public static int[][] industry = new int[][]
        {
            new int [] { 38, 50, 0, 0, -1,   70, 20, 10,  0,   28,  90, 100, 20, 220,   300, 300,   100 },
            new int [] { 35, 50, 0, 0, -1,   20, 45, 25, 10,   30, 100, 110, 18, 235,   150, 150,   140 },
            new int [] { 32, 50, 0, 0, -1,    5, 20, 45, 30,   32, 110, 120, 16, 250,    25,  50,   160 }
        };

        public static int[][] industryFarm = new int[][]
        {
            new int [] { 250, 50, 0, 0, -1,   90, 10,  0, 0,   10,  80, 100, 20, 180,   0, 175,    50 },
            new int [] { 55, 25, 0, 0, -1,   30, 60, 10, 0,   40, 100, 150, 25, 220,   0, 180,   100 }
        };

        public static int[][] industryForest = new int[][]
        {
            new int [] { 160, 50, 0, 0, -1,   90, 10,  0, 0,   20, 25, 35, 20, 180,   0, 210,    50 },
            new int [] { 45, 20, 0, 0, -1,   30, 60, 10, 0,   60, 70, 80, 30, 240,   0, 200,   100 }
        };

        public static int[][] industryOre = new int[][] 
        {
            new int [] { 80, 50, 0, 0, -1,   18, 60, 20,  2,    50, 100, 100, 50, 250,   400, 500,    75 },
            new int [] { 40, 30, 0, 0, -1,   15, 40, 35, 10,   120, 160, 170, 40, 320,   300, 475,   100 }
        };

        public static int[][] industryOil = new int[][]
        {
            new int [] { 80, 50, 0, 0, -1,   15, 60, 23,  2,    90, 180, 220, 40, 300,   450, 375,    75 },
            new int [] { 38, 30, 0, 0, -1,   10, 35, 45, 10,   180, 200, 240, 50, 400,   300, 400,   100 }
        };
    }
}
