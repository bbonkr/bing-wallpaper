import { combineReducers } from 'redux';
import imagesState from './images';

const rootState = combineReducers({
    images: imagesState,
});

export default rootState;
export type RootState = ReturnType<typeof rootState>;
