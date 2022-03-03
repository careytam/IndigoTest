REMARKS
I would like to mention that this program is fully functional.  It is tested mainly on the keyboard set of text characters.  I only ran one test on a text file that have slightly bigger dimensions than the keyboard
set of characters which worked so you canb basically supply this program with a text file of any size and it will work.  All the unit tests runs and passes.  I did not have enough time to test everything thouroughly
and I am able to further perfect this program if I had enough time but everythign is basically there.

HOW TO RUN EXECUTABLE
-The executable is published into a directory called "Publish" in the root directory
-Open a command prompt and cd into the Publish directory
-Then run the following command for a successful test case for the keyboard set of characters:

	C:\Coding\IndigoCodingTest\Publish>KeyboardTransformation.exe KeyboardText.txt TransformationCommand.txt

-Then if you would like to test a case where validations were triggered meaning that there was a issue with either the text or command file, you can run this command:

	C:\Coding\IndigoCodingTest\Publish>KeyboardTransformation.exe NotEveryLineSameNumberTextFile.txt TransformationCommandNoValidCommand.txt

-I have one test case that utilizes a text file with a slightly bigger dimension that you can use by running the following command:

	C:\Coding\IndigoCodingTest\Publish>KeyboardTransformation.exe BiggerDimensionText.txt BiggerDimensionCommand.txt

-If you would like to use you own text and command files, you will have to place it into the TestTextFiles folder within the Publish folder at:

	C:\Coding\IndigoCodingTest\Publish\TestTextFiles

-If you text and command files passes validation and if the transformations gets applied to the text file you supplied, the results of what gets printed in the screen can also be found as a text file exported into
 a folder called Results folder in the TestTextFiles folder.  For example this Results folder is located on my local box as:

	C:\Coding\IndigoCodingTest\Publish\TestTextFiles\Results

-A few thing you have to watch out for are:
	-Your text file has to contains the same number of characters for every line and has to have a minumum number of 2 lines
	-The command file can only contain one line of commands
	-The command file can contain commands apart from V, H and integers so if you have any invalid commands embedded in command file like 'C', all invalid commands will be ignored
	-The command file has to have at least one command or if there is more than one command, it has to have at least one valid command

HISTORY
I at first misread the question and thought that the program will only apply to a keyboard set of characters and therefore, my initial implementations was too hard coded as implemented in Transformation.cs and
unable to handle text files with dynamic sizes.  Then when I realized that the program has to be flexible with dynamic sized text files, I overrided Transformation.cs with DynamkicTransformation.cs.  I did the 
same with corresponding test cases within the test project.

DESIGN PATTERNS - CONSOLE PROJECT
As mentioned by Graham, you would like to see how I structured my code so I employed OOP.  In the console project, I have 3 main clases which are:

	-DynamicTransformation.cs which handles all text file related operations including the actual transformation operations
	-TransformationFiles.cs which handles all operations that has to be performed on the transformation file
	-Then using the facade design pattern, I have pulled the above 2 mentioned classes into TransformationFiles.cs class and consolidated the interaction of the text and command file logic together

The TransformationFiles.cs class also holds the static main which is the entry point of the console program.  Since the static Main fucntion is of static scope, it can instantiate an instance of itself in it which
further calls on the operations provided in out TransformationFiles.cs facade to complete the closing of the program logic.

DESIGN PATTERNS - UNIT TEST PROJECT
As mentioned, I initally misread the question and thought that text files were set to be the keyboard set of characters.  Therefore, the class I intially wrote (Transformation.cs) was tested in 
TransformationTests.cs.  After when I realized that text files has to be dynamic, I wrote DynamicTransformation.cs which was tested in DynamicKeyboardTransformationTests.cs.  However, I wanted to 
make my intital testing with the same test cases from the set of characters of the keyboard, so in DynamicKeyboardTransformationTests.cs, so I inherited TransformationTests.cs from 
DynamicKeyboardTransformationTests.cs, the only difference is I changed "protected Transformation transformation" to use a instance of DynamicTransformation in DynamicKeyboardTransformationTests.cs in the
constructor of DynamicKeyboardTransformationTests.cs and activated the polymorphic behaviour of the "protected Transformation transformation" in it's parent class to reuse all the test cases in the parent
test class.

ROOM FOR IMPROVEMENT
As mentioned, I did not have enough time to perfect this program and here is what I think I can improve:

-I have implemented handled exceptions in the validation methods to ensure integrity of the files, but did not program around unhandled exceptions especially if a wrong file path that does not exist is passed in
-There is some code that is repeated multiple times both in the console and unit test project to get the current directory of where the program is executing out of that should be placed in a static utilities class
 for it to be reused
-For the TransformationCommandTests.cs and TransformationFilesTests.cs unit test classes, I could have refractored it the instatiation of the TransformationCommand and TransformationFiles to be situated in
 the contructor of the corresponding test class in effort to reduce duplicated code
-In TransformationFilesTests.cs, I could have done some more boundary tested and white box testing to the operation method and could have done more testing with text files of different dimensions