import { ActionType } from 'typesafe-actions';
import images from './images';

const rootAction = {
    images: images,
};

export default rootAction;
export type RootAction = ActionType<typeof rootAction>;
