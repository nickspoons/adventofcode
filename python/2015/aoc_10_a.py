""" Advent of Code, 2015: Day 10, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def look_and_say(seq, times):
    """ Recursively play look-and-say on the sequence """
    if times == 0:
        return seq
    newseq = ''
    currentc = ''
    count = 1
    for c in seq:
        if c == currentc:
            count += 1
        else:
            if currentc != '':
                newseq += str(count) + currentc
            currentc = c
            count = 1
    newseq += str(count) + currentc
    return look_and_say(newseq, times - 1)


def run():
    """ Play the look-and-say game 40 times """
    return len(look_and_say(inputs[0], 40))


if __name__ == "__main__":
    print(run())
