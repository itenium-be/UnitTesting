package be.itenium.interview_exercises.generics;

import be.itenium.interview_exercises.generics.model.BaseEntity;
import lombok.Data;

import java.io.Serializable;
import java.util.Collection;

public class GenericsExercise {

	/**
	 * {@link Limited} is a container class that allows storing an actual value along with possible min and max values.
	 * It is a special form of triple. All three values have a generic type that should be a subclass of {@link Number}.
	 *
	 * @param <T> – actual, min and max type
	 */
	@Data
	public static class Limited<T extends Number> {
		private final T actual;
		private final T min;
		private final T max;
	}

	/**
	 * {@link StrictProcessor} defines a contact of a processor that can process only objects that are {@link Serializable}
	 * and {@link Comparable}.
	 *
	 * @param <T> – the type of objects that can be processed
	 */
	interface StrictProcessor<T extends Serializable & Comparable<T>> {
		void process(T obj);
	}

	/**
	 * {@link CollectionRepository} defines a contract of a runtime store for entities based on any {@link Collection}.
	 * It has methods that allow to save a new entity, and to retrieve the whole collection.
	 *
	 * @param <T> – a type of the entity that should be a subclass of {@link BaseEntity}
	 * @param <C> – a type of any collection
	 */
	interface CollectionRepository<T extends BaseEntity, C extends Collection<T>> {
		void save(T entity);

		C getEntityCollection();
	}

}
