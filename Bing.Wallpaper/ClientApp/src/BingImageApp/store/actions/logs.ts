import { ActionType, createAction, createAsyncAction } from 'typesafe-actions';
import {
    LoadLogsRequestModel,
    LogsApiResponseModel,
    ApiResponseModel,
} from '../../models';

export const loadLogs = createAsyncAction(
    'load-logs/request',
    'load-logs/success',
    'load-logs/failure',
)<LoadLogsRequestModel, LogsApiResponseModel, ApiResponseModel>();

export const appendLogs = createAsyncAction(
    'append-logs/request',
    'append-logs/success',
    'append-logs/failure',
)<LoadLogsRequestModel, LogsApiResponseModel, ApiResponseModel>();

export const resetLoadLogsError = createAction('load-logs/reset-error')();

const logsActions = {
    loadLogs,
    appendLogs,
    resetLoadLogsError,
};

export type LogsActionTypes =
    | ActionType<typeof loadLogs>
    | ActionType<typeof appendLogs>
    | ActionType<typeof resetLoadLogsError>;

export default logsActions;
