import { combineEpics } from 'redux-observable';
import imagesEpic from './images';
import logsEpic from './logs';
import bingImagesEpic from './bingImages';

const rootEpic = combineEpics(imagesEpic, logsEpic, bingImagesEpic);
export default rootEpic;
export type RootEpic = ReturnType<typeof rootEpic>;
