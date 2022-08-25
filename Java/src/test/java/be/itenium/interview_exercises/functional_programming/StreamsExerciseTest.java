package be.itenium.interview_exercises.functional_programming;

import static org.junit.jupiter.api.Assertions.*;

import be.itenium.interview_exercises.util.model.Account;
import be.itenium.interview_exercises.util.model.Sex;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.MethodOrderer;
import org.junit.jupiter.api.Order;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.TestMethodOrder;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;

@TestMethodOrder(MethodOrderer.OrderAnnotation.class)
class StreamsExerciseTest {

	private StreamsExercise streams;
	private static List<Account> accounts = Arrays.asList(
			new Account(1L, "Justin", "Butler", "justin.butler@gmail.com",
					LocalDate.parse("2003-04-17"), Sex.MALE, LocalDate.parse("2016-06-13"), BigDecimal.valueOf(172966)),
			new Account(2L, "Olivia", "Cardenas", "cardenas@mail.com",
					LocalDate.parse("1930-01-19"), Sex.FEMALE, LocalDate.parse("2014-06-21"), BigDecimal.valueOf(38029)),
			new Account(3L, "Nolan", "Donovan", "nolandonovan@gmail.com",
					LocalDate.parse("1925-04-19"), Sex.MALE, LocalDate.parse("2011-03-10"), BigDecimal.valueOf(13889)),
			new Account(4L, "Lucas", "Lynn", "lucas.lynn@yahoo.com",
					LocalDate.parse("1987-05-25"), Sex.MALE, LocalDate.parse("2009-03-05"), BigDecimal.valueOf(16980))
	);

	@BeforeEach
	void setUp() {
		streams = new StreamsExercise(accounts);
	}

	@Test
	@Order(1)
	void findRichestPerson() {
		Optional<Account> expectedPerson = Optional.of(accounts.get(0));
		Optional<Account> actualRichestPerson = streams.findRichestPerson();

		assertEquals(expectedPerson, actualRichestPerson);
	}

	@Test
	@Order(2)
	void separateAccountsBySex() {
		Map<Sex, List<Account>> expectedAccountMap = getExpectedMap();
		Map<Sex, List<Account>> maleToAccountsMap = streams.partitionAccountsBySex();

		assertEquals(expectedAccountMap, maleToAccountsMap);
	}

	private Map<Sex, List<Account>> getExpectedMap() {
		Map<Sex, List<Account>> expectedMap = new HashMap<>(2);
		expectedMap.put(Sex.MALE, Arrays.asList(accounts.get(0), accounts.get(2), accounts.get(3)));
		expectedMap.put(Sex.FEMALE, Arrays.asList(accounts.get(1)));
		return expectedMap;
	}

	@Test
	@Order(3)
	void containsAccountWithEmailDomain() {
		assertTrue(streams.containsAccountWithEmailDomain("gmail.com"));
		assertTrue(streams.containsAccountWithEmailDomain("yahoo.com"));
		assertFalse(streams.containsAccountWithEmailDomain("ukr.net"));
	}

	@Test
	@Order(4)
	void collectBalancesByEmailForAccountsCreatedOn() {
		Account account = accounts.get(3);

		Map<String, BigDecimal> emailToBalanceMap = streams.collectBalancesByEmailForAccountsCreatedOn(account.getCreationDate().getYear());

		assertEquals(Map.of(account.getEmail(), account.getBalance()), emailToBalanceMap);
	}

	@Test
	@Order(5)
	void getCharacterFrequencyInFirstNames() {
		Map<Character, Long> characterFrequencyInFirstAndLastNames = streams.getCharacterFrequencyInFirstNames();

		assertEquals(3, characterFrequencyInFirstAndLastNames.get('a').longValue());
		assertEquals(1, characterFrequencyInFirstAndLastNames.get('c').longValue());
		assertEquals(3, characterFrequencyInFirstAndLastNames.get('i').longValue());
		assertEquals(1, characterFrequencyInFirstAndLastNames.get('J').longValue());
		assertEquals(1, characterFrequencyInFirstAndLastNames.get('L').longValue());
		assertEquals(2, characterFrequencyInFirstAndLastNames.get('l').longValue());
		assertEquals(2, characterFrequencyInFirstAndLastNames.get('u').longValue());
	}
}
