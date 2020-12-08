""" Advent of Code, 2020: Day 08, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Toggle jmp/nop and run until sequence completes with no loop """
    commands = []
    for line in inputs:
        n = int(line[5:])
        if line[4] == "-":
            n = 0 - n
        commands.append([line[:3], n])

    for command in commands:
        if command[0] == "acc":
            continue
        orig = command[0]
        command[0] = "jmp" if orig == "nop" else "nop"

        acc, pos = 0, 0
        seen = set()
        while True:
            if pos == len(commands):
                return acc
            if pos in seen:
                break
            seen.add(pos)
            if commands[pos][0] == "jmp":
                pos += commands[pos][1]
                continue
            if commands[pos][0] == "acc":
                acc += commands[pos][1]
            pos += 1

        command[0] = orig

    return 0


if __name__ == "__main__":
    print(run())
