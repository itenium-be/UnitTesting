import createAdder from '../3-HOF';

describe('HOF', () => {
  it('can create an addSix', () => {
    // Arrange
    const addSix = createAdder(6);

    // Act
    const result = addSix(10);

    // Asert
    expect(result).toBe(16);
  });

  it('can create an addSeven', () => {
    const addSeven = createAdder(7);
    const result = addSeven(3);
    expect(result).toBe(10);
  });
});
