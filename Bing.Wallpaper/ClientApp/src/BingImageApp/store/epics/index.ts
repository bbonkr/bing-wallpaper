import { combineEpics } from 'redux-observable';
import imagesEpic from './images';

const rootEpic = combineEpics(imagesEpic);
export default rootEpic;
export type RootEpic = ReturnType<typeof rootEpic>;
