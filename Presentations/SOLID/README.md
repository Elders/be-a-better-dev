	#SOLID#
	1. Single Responsibility Principle
		a. A class should have only one reason to change. (There can be only one requirement that, when changed, will cause a class to change.)
		b. Benefits
	2. Open/Close Principle
		a. Software entities should be open for extension, but closed for modification. (Once a class is done, it is DONE!)
		b. Meyer vs. Polymorphic
		c. Benefits
	3. Liskovs Substitution Principle
		a. Let q(x) be a property provable about objects x of type T. Then q(y) should be provable for objects y of type S where S is a subtype of T. (A subclass should behave in such a way that it will not cause problems when used instead the superclass)
		b. Rules:
			i. Contravariance of method arguments in sub class is allowed.
			ii. Covariance of return types in sub class is not allowed.
			iii. No new exceptions types are allowed to be thrown, unless they are sub classes of previously used ones.
			iv. Preconditions cannot be strengthened in a subtype. Postconditions cannot be weakened in a subtype.
			v. The history constraint(not allowed to change mutable to immutable and vice versa.)
		c. Benefits
	4. Interface Segregation Principle
		a. Clients should not be forced to depend upon interfaces that they don't use.
		b. Benefits
	5. Dependency Inversion Principle
		a. High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend upon details. Details should depend upon abstractions. (By making sure classes don't depend on specific implementations, it becomes easy to change things around.)
		b. Benefits
