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

	}
}
