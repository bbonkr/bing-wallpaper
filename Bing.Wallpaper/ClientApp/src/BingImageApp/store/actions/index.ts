import { ActionType } from 'typesafe-actions';
import images from './images';
import logs from './logs';

const rootAction = {
    images,
    logs,
};

export default rootAction;
export type RootAction = ActionType<typeof rootAction>;
