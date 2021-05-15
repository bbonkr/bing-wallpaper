import { combineReducers } from 'redux';
import imagesState from './images';
import logsState from './logs';

const rootState = combineReducers({
    images: imagesState,
    logs: logsState,
});

export default rootState;
export type RootState = ReturnType<typeof rootState>;
