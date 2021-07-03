import { ActionType } from 'typesafe-actions';
import { imagesActions } from './images';
import { logsActions } from './logs';
import { bingImagesActions } from './bingImages';

const rootAction = {
    bingImages: bingImagesActions,
    images: imagesActions,
    logs: logsActions,
};

export default rootAction;
export type RootAction = ActionType<typeof rootAction>;
