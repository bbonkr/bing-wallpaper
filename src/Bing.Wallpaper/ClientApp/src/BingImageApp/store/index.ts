import { useMemo } from 'react';
import { applyMiddleware, createStore, Store } from 'redux';
import { createEpicMiddleware } from 'redux-observable';
import { composeWithDevTools } from 'redux-devtools-extension';
import { RootAction } from './actions';
import rootEpic from './epics';
import rootState, { RootState } from './reducers';
import { ApiClient } from '../services';

const initStore = (preloadedState?: RootState) => {
    const epicMiddleware = createEpicMiddleware<
        RootAction,
        RootAction,
        RootState,
        ApiClient
    >({
        dependencies: new ApiClient(),
    });

    const middlewares = [epicMiddleware];
    const enhancer = composeWithDevTools(applyMiddleware(...middlewares));

    const store = createStore(rootState, preloadedState, enhancer);

    epicMiddleware.run(rootEpic);

    return store;
};

let __store: Store | undefined;

export const initialStore = (preloadedState?: RootState) => {
    let _store = __store ?? initStore(preloadedState);

    if (preloadedState && __store) {
        _store = initStore({
            ...__store.getState(),
            ...preloadedState,
        });

        __store = undefined;
    }

    if (typeof window === 'undefined') {
        return _store;
    }

    if (!__store) {
        __store = _store;
    }

    return _store;
};

export const useStore = (initialState?: RootState) => {
    const store = useMemo(() => initialStore(initialState), [initialState]);

    return store;
};
