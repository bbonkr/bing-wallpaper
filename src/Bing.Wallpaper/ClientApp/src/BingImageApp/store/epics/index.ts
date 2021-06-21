import { combineEpics } from 'redux-observable';
import imagesEpic from './images';
import logsEpic from './logs';

const rootEpic = combineEpics(imagesEpic, logsEpic);
export default rootEpic;
export type RootEpic = ReturnType<typeof rootEpic>;
