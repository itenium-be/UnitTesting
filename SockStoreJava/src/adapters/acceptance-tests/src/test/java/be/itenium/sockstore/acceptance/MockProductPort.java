package be.itenium.sockstore.acceptance;

import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.vocabulary.ProductId;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.function.Function;

public class MockProductPort implements ProductPort {
    private final List<MethodCall> methodCalls = new ArrayList<>();
    private ProductAggregate saveReturnValue;
    private Optional<ProductAggregate> findByIdReturnValue = Optional.empty();
    private List<ProductAggregate> findAllReturnValue = new ArrayList<>();

    // Method call tracking
    public record MethodCall(String methodName, Object[] arguments) {}

    @Override
    public ProductAggregate save(ProductAggregate product) {
        methodCalls.add(new MethodCall("save", new Object[]{product}));
        return saveReturnValue != null ? saveReturnValue : product;
    }

    @Override
    public Optional<ProductAggregate> findById(ProductId id) {
        methodCalls.add(new MethodCall("findById", new Object[]{id}));
        return findByIdReturnValue;
    }

    @Override
    public List<ProductAggregate> findAll() {
        methodCalls.add(new MethodCall("findAll", new Object[]{}));
        return new ArrayList<>(findAllReturnValue);
    }

    // Setup methods for test configuration
    public MockProductPort whenSave(ProductAggregate returnValue) {
        this.saveReturnValue = returnValue;
        return this;
    }

    public MockProductPort whenFindById(Optional<ProductAggregate> returnValue) {
        this.findByIdReturnValue = returnValue;
        return this;
    }

    public MockProductPort whenFindAll(List<ProductAggregate> returnValue) {
        this.findAllReturnValue = new ArrayList<>(returnValue);
        return this;
    }

    // Verification methods
    public boolean wasMethodCalled(String methodName) {
        return methodCalls.stream().anyMatch(call -> call.methodName().equals(methodName));
    }

    public int getMethodCallCount(String methodName) {
        return (int) methodCalls.stream()
                .filter(call -> call.methodName().equals(methodName))
                .count();
    }

    public List<Object[]> getMethodCallArguments(String methodName) {
        return methodCalls.stream()
                .filter(call -> call.methodName().equals(methodName))
                .map(MethodCall::arguments)
                .toList();
    }

    public void reset() {
        methodCalls.clear();
        saveReturnValue = null;
        findByIdReturnValue = Optional.empty();
        findAllReturnValue = new ArrayList<>();
    }

    // Convenience verification methods
    public boolean wasSaveCalled() {
        return wasMethodCalled("save");
    }

    public boolean wasFindByIdCalled() {
        return wasMethodCalled("findById");
    }

    public boolean wasFindAllCalled() {
        return wasMethodCalled("findAll");
    }

    public ProductAggregate getLastSavedProduct() {
        List<Object[]> saveArgs = getMethodCallArguments("save");
        if (saveArgs.isEmpty()) {
            return null;
        }
        Object[] lastArgs = saveArgs.get(saveArgs.size() - 1);
        return (ProductAggregate) lastArgs[0];
    }

    public ProductId getLastFindByIdArgument() {
        List<Object[]> findByIdArgs = getMethodCallArguments("findById");
        if (findByIdArgs.isEmpty()) {
            return null;
        }
        Object[] lastArgs = findByIdArgs.get(findByIdArgs.size() - 1);
        return (ProductId) lastArgs[0];
    }
}
