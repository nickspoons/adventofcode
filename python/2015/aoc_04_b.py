""" Advent of Code, 2015: Day 04, b """

import hashlib

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
word = inputs[0].strip()


def run():
    """ Brute force: generating md5 hashes of strings """
    n = 0
    while True:
        h = hashlib.md5((word + str(n)).encode("utf-8")).hexdigest()
        if h[:6] == "000000":
            return n
        n += 1

    return 0


if __name__ == "__main__":
    print(run())
