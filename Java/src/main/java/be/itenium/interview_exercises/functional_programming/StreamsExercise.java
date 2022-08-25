package be.itenium.interview_exercises.functional_programming;

import be.itenium.interview_exercises.util.model.Account;
import be.itenium.interview_exercises.util.model.Sex;
import lombok.AllArgsConstructor;

import java.math.BigDecimal;
import java.util.Collection;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.function.Function;
import java.util.stream.Collectors;

@AllArgsConstructor
public class StreamsExercise {

	private Collection<Account> accounts;

	/**
	 * Returns {@link Optional} that contains an {@link Account} with the highest balance value
	 *
	 * @return account with max balance wrapped with optional
	 */
	public Optional<Account> findRichestPerson() {
		return accounts.stream()
				.max(Comparator.comparing(Account::getBalance));
	}

	/**
	 * Returns a map that separates all accounts into two lists - male and female.
	 *
	 * @return a map where key is a Sex, and value is list of male, and female accounts
	 */
	public Map<Sex, List<Account>> partitionAccountsBySex() {
		return accounts.stream()
				.collect(Collectors.groupingBy(Account::getSex));
	}

	/**
	 * Returns a {@link List} of {@link Account} objects sorted by first and last name.
	 *
	 * @return list of accounts sorted by first and last name
	 */
	public List<Account> sortByFirstAndLastNames() {
		return accounts.stream()
				.sorted(Comparator.comparing(Account::getFirstName).thenComparing(Account::getLastName))
				.collect(Collectors.toList());
	}

	/**
	 * Checks if there is at least one account with provided email domain.
	 *
	 * @param emailDomain
	 * @return true if there is an account that has an email with provided domain
	 */
	public boolean containsAccountWithEmailDomain(String emailDomain) {
		return accounts.stream()
				.map(Account::getEmail)
				.anyMatch(e -> e.split("@")[1].equals(emailDomain));
	}

	/**
	 * Filters accounts by the year when an account was created. Collects account balances by email into a {@link Map}.
	 * The key is {@link Account#email} and the value is {@link Account#balance}
	 *
	 * @param year the year of account creation
	 * @return map of balances by its email, created in a particular year
	 */
	public Map<String, BigDecimal> collectBalancesByEmailForAccountsCreatedOn(int year) {
		return accounts.stream()
				.filter(account -> account.getCreationDate().getYear() == year)
				.collect(Collectors.toMap(Account::getEmail, Account::getBalance));
	}

	/**
	 * Returns a {@link Map} where key is a letter {@link Character}, and value is a number of its occurrences in
	 * {@link Account#firstName}.
	 *
	 * @return a map where key is a letter and value is the amount of occurrences in all first names
	 */
	public Map<Character, Long> getCharacterFrequencyInFirstNames() {
		return accounts.stream()
				.map(Account::getFirstName)
				.flatMapToInt(String::chars)
				.mapToObj(c -> (char) c)
				.collect(Collectors.groupingBy(Function.identity(), Collectors.counting()));
	}

}
