import { ActionType } from 'typesafe-actions';
import { imagesActions } from './images';
import { logsActions } from './logs';

const rootAction = {
    images: imagesActions,
    logs: logsActions,
};

export default rootAction;
export type RootAction = ActionType<typeof rootAction>;
