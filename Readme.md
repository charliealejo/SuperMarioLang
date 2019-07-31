# SuperMarioLang

An interpreter in C# for the MarioLang esoteric language, plus some added features.

## Description of the language

Just imagine a crossover between Brainfuck and a level of Super Mario Bros. The code is laid over a
2D ASCII-art representation of a Mario level, with floor tiles, elevators and instructions along the way.
The original idea of MarioLang comes from user [Wh1teWolf](https://esolangs.org/wiki/User:Wh1teWolf) in
the [esolangs.org](http://esolangs.org) site.

Following is a "Hello World" code written in MarioLang:

                   MarioLANG Hello World    <
             = =============================^<
               |   Created by JWinslow23   |=^<
             = ==============================="=
    ++++++++++
    ========== =
              >)+++++++)++++++++++)+++)+((((-[!)++.)+.
             =================================#======= =
      .------.+++.).+++++++++++++++((.++).+++..+++++++<
    = ==================================================
     >--------.)+.).
    ================ =         |*
                              $$$
                             $$$$$
                             $$ $$
                    >        $$ $$
                    =========== ======

The instruction pointer starts in the topmost, leftmost position of the scenario, considered to be
the origin of coordinates (0,0). Mario starts there and walking to the right, unless there is no floor
beneath him, in that case he starts falling until he hits the ground and the starts walking to the right.
Mario's movements are considered to be the instruction pointer of the program. The memory layout is
similar to that of Brainfuck: a circular tape with 256 positions, where every position can store an integer
(32-bit precision) value. Tape starts at position 0.

The program execution ends when Mario goes out of the scenario, either falling below the bottom line, going
beyond the rightmost or leftmost part of the scenario, or jumping up above the first line of the scenario.
The execution also stops when Mario gets stuck at any place of the scenario, but in this case it is
considered an abnormal situation and an error message will be displayed.

## Instructions

Along Mario's way he can step over several kind of instructions:

* `)` __TAPE_RIGHT__: Moves the tape pointer one position to the right.
* `(` __TAPE_LEFT__: Moves the tape pointer one position to the left.
* `+` __TAPE_INCR__: Increments the value at the tape position in 1 unit.
* `-` __TAPE_DECR__: Decrements the value at the tape position in 1 unit.
* `%` __TAPE_JUMP__: Moves the tape pointer to the position specified by the current value. _(Example: we
are at cell #7 with a value of 2, so the tape pointer moves to cell #2.)_
* `&` __TAPE_INDEX__: Sets the current value equals to the current tape position. _(Example: we are at
cell #4, so we set the value at cell #4 to 4.)_
* `*` __TAPE_RETRIEVE__: Sets the current value equals to the value stored in the position pointed by
the current value. _(Example: we are at cell #9 with a value of 3, so we set the value at cell #9
equals to the value at cell #3.)_
* `.` __WRITE_CHAR__: Writes the value at the tape position in the standard output as a character
by its ASCII code.
* `:` __WRITE_NUMBER__: Writes the value at the tape position in the standard output as a number.
* `,` __READ_CHAR__: Reads a character from the standard input and stores its ASCII code in the current
position of the tape. If there are no arguments to read, a 0 (zero) value is read instead.
* `;` __READ_NUMBER__: Parses a number from the standard input and stores it in the current position of
the tape. If there are no arguments to read, a 0 (zero) value is read instead.

## Scenario tiles

* `=` __FLOOR__: A floor tile. Mario can stand over these and cannot go through them.
* `|` __WALL__: A wall tile. Mario can stand over these and cannot go through them.
* `#` __ELEVATOR_START__: The start position of an elevator. When Mario stops over one of these tiles
the elevator will start moving toward its end position. If Mario does not stop over the elevator it 
will not move and Mario will keep on walking left or right.
* `"` __ELEVATOR_END__: The destination of an elevator. Above these tiles it is recommended to place
a GO_RIGHT or GO_LEFT command to Mario.

A note about elevators: multiple elevators in one column of code are not supported.
 
## Commands to Mario

* `>` __GO_RIGHT__: Makes Mario move to the right of the scenario.
* `<` __GO_LEFT__: Makes Mario move to the left of the scenario.
* `^` __JUMP__: Makes Mario jump one position up in the scenario.
* `!` __STOP__: Makes Mario stop. Usually placed above elevators, otherwise the code will loop indefinitely.
* `[` __BRANCH__: Makes Mario skip the next command if the current value pointed by the tape is 0.
* `@` __TURN_AROUND__: Makes Mario change direction.

## Comments

Every character not specified above is considered to be a NO-OP, so they can be used to write
comments.

## Examples

### Hello, world! (Revisited)

                SuperMarioLANG Hello World  <
             = =============================^<
               |   Created by Charlie      |=^<
    +        = ==============================="=
    ++++++++++
    ========== =
              >)+++)+++++++)+++++++++)++++)%-[!))-----.)++
             =================================#==========
    -(.+++*-&).(.++++++++*-&.-*+)).(.+++..+++++++*--&)). <
     =====================================================
    > --.((-.((.
    ============= =        |*
                          $$$
                         $$$$$
                         $$ $$
                 >       $$ $$
                =========== ======

## Command-line arguments

The code compiles to a DLL file that can be run with the `dotnet` command from .NET, along
with some command-line parameters. You can get the following help message if you run
the application with no arguments:

    SuperMarioLang interpreter.
    
    Usage:
    
        SuperMarioLang [-d] [-s N] <FileToOpen> [<args>]
    
    Parameters:
        -d            Debug mode, writes information about what Mario did
        -s N          Sets the tape size to N bytes
        <FileToOpen>  The path to the file with the code to execute
        <args>        The optional arguments to pass to the code

The only mandatory argument is the file to read as source code. Every other argument
is optional, including the arguments for the code itself.
