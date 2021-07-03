import { combineReducers } from 'redux';
import imagesState from './images';
import logsState from './logs';
import bingImagesState from './bingImages';

const rootState = combineReducers({
    images: imagesState,
    logs: logsState,
    bingImages: bingImagesState,
});

export default rootState;
export type RootState = ReturnType<typeof rootState>;
