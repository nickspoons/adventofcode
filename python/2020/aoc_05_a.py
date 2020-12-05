""" Advent of Code, 2020: Day 05, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Translate the column/row leters to 0/1 and read as binary integers """
    largest = 0
    for seat in inputs:
        row = int(seat[:7].replace("F", "0").replace("B", "1"), 2)
        column = int(seat[7:].replace("L", "0").replace("R", "1"), 2)
        seat_id = row * 8 + column
        if seat_id > largest:
            largest = seat_id
    return largest


if __name__ == "__main__":
    print(run())
