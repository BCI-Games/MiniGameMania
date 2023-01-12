using UnityEngine;

class Randomizer {
    public static int RangeNot(int minInclusive, int maxExclusive, int exclude) {
        int value = exclude;
        while(value == exclude) {
            value = Random.Range(minInclusive, maxExclusive);
        }

        return value;
    }

    public static bool RandomBool() {
        int value = Random.Range(0, 2);
        return value == 0;
    }
}