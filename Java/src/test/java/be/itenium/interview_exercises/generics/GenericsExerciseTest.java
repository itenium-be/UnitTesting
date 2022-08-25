package be.itenium.interview_exercises.generics;

import static org.assertj.core.api.Assertions.assertThat;

import be.itenium.interview_exercises.generics.GenericsExercise.CollectionRepository;
import be.itenium.interview_exercises.generics.GenericsExercise.Limited;
import be.itenium.interview_exercises.generics.GenericsExercise.StrictProcessor;
import be.itenium.interview_exercises.generics.model.BaseEntity;
import lombok.SneakyThrows;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.MethodOrderer;
import org.junit.jupiter.api.Order;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.TestMethodOrder;

import java.io.Serializable;
import java.lang.reflect.Method;
import java.util.Arrays;
import java.util.Collection;

@TestMethodOrder(MethodOrderer.OrderAnnotation.class)
class GenericsExerciseTest {

	final String TYPE_PARAMETER_NAME = "T";

	@Test
	@Order(1)
	@DisplayName("Limited class has one type parameter")
	void limitedClassHasOneTypeParameter() {
		var typeParameters = Limited.class.getTypeParameters();

		assertThat(typeParameters).hasSize(1);
	}

	@Test
	@Order(2)
	@DisplayName("Limited class type parameter is bounded by Number")
	void limitedClassTypeParameterIsBoundedByNumber() {
		var typeParameters = Limited.class.getTypeParameters();
		var typeParam = typeParameters[0];
		assert (typeParam.getBounds().length == 1);
		var boundType = typeParam.getBounds()[0];

		assertThat(boundType.getTypeName()).isEqualTo(Number.class.getTypeName());
	}

	@Test
	@SneakyThrows
	@Order(3)
	@DisplayName("Limited class fields have generic type \"T\"")
	void limitedClassFieldsAreGeneric() {
		var fields = Limited.class.getDeclaredFields();

		for (var f : fields) {
			assertThat(f.getGenericType().getTypeName()).isEqualTo(TYPE_PARAMETER_NAME);
		}
	}

	@Test
	@Order(4)
	@DisplayName("StrictProcessor class has one generic type")
	void strictProcessorHasOneGenericType() {
		var typeParameters = StrictProcessor.class.getTypeParameters();

		assertThat(typeParameters).hasSize(1);
	}

	@Test
	@Order(5)
	@DisplayName("StrictProcessor type parameter is called \"T\"")
	void strictProcessorTypeParameterIsCalledT() {
		var typeParameters = StrictProcessor.class.getTypeParameters();
		var typePram = typeParameters[0];

		assertThat(typePram.getTypeName()).isEqualTo(TYPE_PARAMETER_NAME);
	}

	@Test
	@Order(6)
	@DisplayName("StrictProcessor type parameter is bound by both Serializable and Comparable<T>")
	void strictProcessorTypeParameterIsBoundBySerializableAndComparable() {
		var typeParameters = StrictProcessor.class.getTypeParameters();
		var typeParam = typeParameters[0];
		assert (typeParam.getBounds().length == 2);
		var serializableBoundType = typeParam.getBounds()[0];
		var comparableBoundType = typeParam.getBounds()[1];

		assertThat(serializableBoundType.getTypeName())
				.isEqualTo(Serializable.class.getTypeName());
		assertThat(comparableBoundType.getTypeName())
				.isEqualTo(String.format("%s<%s>", Comparable.class.getTypeName(), TYPE_PARAMETER_NAME));
	}

	@Test
	@Order(7)
	@DisplayName("StrictProcessor process method parameter has type \"T\"")
	void strictProcessorProcessMethodParameterHasTypeT() {
		var processMethod = getMethodByName(StrictProcessor.class, "process");

		assert (processMethod.getParameters().length == 1);
		var processMethodParam = processMethod.getParameters()[0];

		assertThat(processMethodParam.getParameterizedType().getTypeName()).isEqualTo(TYPE_PARAMETER_NAME);
	}

	@Test
	@Order(26)
	@DisplayName("CollectionRepository has two type parameters")
	void collectionRepositoryHasTwoTypeParameters() {
		var typeParameters = CollectionRepository.class.getTypeParameters();

		assertThat(typeParameters).hasSize(2);
	}

	@Test
	@Order(27)
	@DisplayName("CollectionRepository first type parameter is called \"T\"")
	void collectionRepositoryFirstTypeParameterIsCalledT() {
		var typeParam = CollectionRepository.class.getTypeParameters()[0];

		assertThat(typeParam.getTypeName()).isEqualTo(TYPE_PARAMETER_NAME);
	}

	@Test
	@Order(28)
	@DisplayName("CollectionRepository first type parameter is bounded by BaseEntity")
	void collectionRepositoryFirstTypeParameterIsBoundedByBaseEntity() {
		var typeParam = CollectionRepository.class.getTypeParameters()[0];
		var boundType = typeParam.getBounds()[0];

		assertThat(boundType.getTypeName()).isEqualTo(BaseEntity.class.getTypeName());
	}

	@Test
	@Order(29)
	@DisplayName("CollectionRepository second type parameter is called \"C\"")
	void collectionRepositorySecondTypeParameterIsCalledT() {
		var typeParam = CollectionRepository.class.getTypeParameters()[1];

		assertThat(typeParam.getTypeName()).isEqualTo("C");
	}

	@Test
	@Order(30)
	@DisplayName("CollectionRepository second type parameter is bounded by Collection<T>")
	void collectionRepositorySecondTypeParameterIsBoundedByCollection() {
		var typeParam = CollectionRepository.class.getTypeParameters()[1];
		var boundType = typeParam.getBounds()[0];

		assertThat(boundType.getTypeName())
				.isEqualTo(String.format("%s<T>", Collection.class.getTypeName()));
	}

	@Test
	@Order(31)
	@DisplayName("CollectionRepository save method param has type \"T\"")
	@SneakyThrows
	void collectionRepositorySaveMethodParameterHasTypeT() {
		var saveMethod = getMethodByName(CollectionRepository.class, "save");

		var methodParam = saveMethod.getParameters()[0];

		assertThat(methodParam.getParameterizedType().getTypeName()).isEqualTo(TYPE_PARAMETER_NAME);
	}

	@Test
	@Order(32)
	@DisplayName("CollectionRepository getEntityCollection method has return type \"C\"")
	@SneakyThrows
	void collectionRepositoryGetCollectionMethodHasReturnTypeC() {
		var getEntityCollectionMethod = CollectionRepository.class.getMethod("getEntityCollection");

		assertThat(getEntityCollectionMethod.getGenericReturnType().getTypeName()).isEqualTo("C");
	}

	private Method getMethodByName(Class<?> clazz, String methodName) {
		return Arrays.stream(clazz.getMethods())
				.filter(m -> m.getName().equals(methodName))
				.findAny().orElseThrow();
	}

}