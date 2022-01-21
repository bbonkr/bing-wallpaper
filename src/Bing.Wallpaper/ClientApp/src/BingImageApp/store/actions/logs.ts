import { ActionType, createAction, createAsyncAction } from 'typesafe-actions';
import {
    ObjectApiResponseModel,
    LogModelPagedModelApiResponseModel,
} from '../../../api/api';
import { LoadLogsRequestModel } from '../../models';

export const loadLogs = createAsyncAction(
    'load-logs/request',
    'load-logs/success',
    'load-logs/failure',
)<
    LoadLogsRequestModel,
    LogModelPagedModelApiResponseModel,
    ObjectApiResponseModel
>();

export const resetLoadLogsError = createAction('load-logs/reset-error')();

export const logsActions = {
    loadLogs,
    resetLoadLogsError,
};

export type LogsActions = ActionType<typeof logsActions>;
