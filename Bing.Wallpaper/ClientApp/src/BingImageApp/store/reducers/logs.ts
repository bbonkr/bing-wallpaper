import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';
import { ApiResponseModel, LogModel } from '../../models';
import logsActions, { LogsActionTypes } from '../actions/logs';

export const isLoadingLogs = createReducer<boolean, LogsActionTypes>(false)
    .handleAction(
        [logsActions.loadLogs.request, logsActions.appendLogs.request],
        (state, action) => true,
    )
    .handleAction(
        [
            logsActions.loadLogs.success,
            logsActions.loadLogs.failure,
            logsActions.appendLogs.success,
            logsActions.appendLogs.failure,
        ],
        (state, action) => false,
    );

export const loadLogsError = createReducer<
    ApiResponseModel | null,
    LogsActionTypes
>(null)
    .handleAction(
        [
            logsActions.loadLogs.request,
            logsActions.loadLogs.success,
            logsActions.appendLogs.request,
            logsActions.appendLogs.success,
            logsActions.resetLoadLogsError,
        ],
        (state, action) => null,
    )
    .handleAction(
        [logsActions.loadLogs.failure, logsActions.appendLogs.failure],
        (state, action) => action.payload,
    );

export const logs = createReducer<LogModel[] | undefined, LogsActionTypes>([])
    .handleAction([logsActions.loadLogs.success], (state, action) => {
        return [...(action.payload.data ?? [])];
    })
    .handleAction([logsActions.appendLogs.success], (state, action) => {
        return [...(state ?? []).concat(action.payload.data ?? [])];
    });

export const hasMoreLogs = createReducer<boolean, LogsActionTypes>(true)
    .handleAction(
        [logsActions.loadLogs.success, logsActions.appendLogs.success],
        (state, action) => (action.payload.data ?? []).length > 0,
    )
    .handleAction(
        [
            logsActions.loadLogs.request,
            logsActions.loadLogs.failure,
            logsActions.appendLogs.request,
            logsActions.appendLogs.failure,
        ],
        (state, action) => true,
    );

const logsState = combineReducers({
    isLoadingLogs,
    logs,
    hasMoreLogs,
    loadLogsError,
});

export default logsState;
export type LogsState = ReturnType<typeof logsState>;
