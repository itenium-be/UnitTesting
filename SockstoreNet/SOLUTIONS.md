Solutions
=========

0.FirstTestsFast
----------------

- Read the Excel once instead of every time (and put in a `Dictionary<Guid, int>`)
- Work with different smaller sets of data instead of the actual production data for testing
- Have a few tests that work with the files that test reading the files into memory correctly
	- And then have most tests work setup the in-memory structures and test from there
- Consider if it's worthwhile writing tests for a one-time migration
	- Ask when this migration will be executed? And will it really be executed only once?




1.FirstTestsIndependent
-----------------------

Typical reasons tests interact with each other:
- Statics are changed in a test and used in another.
- Database records are updated in a test and not expected to have changed in another.

In this case the GlobalDiscount is set in one test and influences other tests.

The easiest solution here is probably to start a transaction and not commit it at
the end of the test.




2.FirstTests
------------

### Repeatable

Typically a dependency on `DateTime.Now`. In this case there is a weekend discount.
We need to introduce an interface `IDateTimeProvider` so that we can control the
"current date" in the tests.


### SelfValidating

An Excel is created and then needs to be manually evaluated.

The same nuget package can be used to validate the resulting Excel.
In case that is not possible, it would also be possible to have an intermediate state
and assert against that state. This is called introducing a `sensing variable` (Working Effectively with Legacy Code)



3.FirstTestsThorough
--------------------





4.FirstTestsTimely
------------------



