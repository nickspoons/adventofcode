""" Advent of Code, 2020: Day 08, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Read operations and perform instructions until loop is detected """
    commands = []
    for line in inputs:
        n = int(line[5:])
        if line[4] == "-":
            n = 0 - n
        commands.append([line[:3], n])

    acc, pos = 0, 0
    seen = set()
    while True:
        if pos in seen:
            break
        seen.add(pos)
        if commands[pos][0] == "jmp":
            pos += commands[pos][1]
            continue
        if commands[pos][0] == "acc":
            acc += commands[pos][1]
        pos += 1

    return acc


if __name__ == "__main__":
    print(run())
