""" Advent of Code, 2015: Day 05, a """

with open(__file__[:-5] + "_input") as f:
    passwords = [p.strip() for p in f]


def validate_vowels(password):
    """ It contains at least three vowels """
    vowels = {'a', 'e', 'i', 'o', 'u'}
    count = 0
    for c in password:
        if c in vowels:
            count += 1
            if count == 3:
                return True
    return False


def validate_doubles(password):
    """ It contains at least one letter that appears twice in a row """
    for i in range(len(password) - 1):
        if password[i] == password[i + 1]:
            return True
    return False


def validate_blacklist(password):
    """ It does not contain the strings ab, cd, pq, or xy """
    for blacklisted in ['ab', 'cd', 'pq', 'xy']:
        if blacklisted in password:
            return False
    return True


def run():
    """ Validate passwords in various ways """
    valid = 0
    for password in passwords:
        if validate_vowels(password) \
                and validate_doubles(password) \
                and validate_blacklist(password):
            valid += 1
    return valid


if __name__ == "__main__":
    print(run())
