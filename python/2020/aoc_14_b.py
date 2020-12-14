""" Advent of Code, 2020: Day 14, b """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def generate_masks(raw):
    """ Create "and" masks and "or" masks for all possible combinations of
    "floating" bits ("X" bits can be either 0 or 1) """
    masks = []
    numx = raw.count("X")
    bits = list(raw)
    for variant in range(2**numx):
        mask = list(f"{bin(variant)[2:]:0>{numx}}")
        maskbits = bits.copy()
        amask = ["1"] * 36
        maskindex = 0
        for i, bit in enumerate(bits):
            if bit == "X":
                maskbits[i] = mask[maskindex]
                if mask[maskindex] == "0":
                    amask[i] = "0"
                maskindex += 1
        omask = int("".join(maskbits), 2)
        amask = int("".join(amask), 2)
        masks.append((omask, amask))
    return masks


def run():
    """ Apply bitmasks with floating bits to all possible addresses """
    memory = dict()
    for line in inputs:
        if line[:4] == "mask":
            masks = generate_masks(line[7:])
        else:
            match = re.match(r"mem\[(\d+)\] = (\d+)$", line)
            for mask in masks:
                address = (int(match.group(1)) | mask[0]) & mask[1]
                memory[address] = int(match.group(2))
    return sum(memory.values())


if __name__ == "__main__":
    print(run())
