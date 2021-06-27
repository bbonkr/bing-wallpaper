import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../store/reducers';
import { LogsState } from '../../store/reducers/logs';
import { logsActions } from '../../store/actions/logs';
import { LoadLogsRequestModel } from '../../models';

export const useLogsApi = () => {
    const dispatch = useDispatch();
    const logs = useSelector<RootState, LogsState>((state) => state.logs);

    return {
        ...logs,
        loadLogsRequest: (payload: LoadLogsRequestModel) =>
            dispatch(logsActions.loadLogs.request(payload)),
        resetLogsError: () => dispatch(logsActions.resetLoadLogsError()),
    };
};

export type UseLogsApi = ReturnType<typeof useLogsApi>;
