import React, { useEffect, useState } from 'react';

interface FormValues {
    level: string;
    keyword: string;
}

export interface FormState {
    values: FormValues;
}

interface LogFilterProps {
    isLoading?: boolean;
    onChange?: (state: FormState) => void;
    onSubmit?: (state: FormState) => void;
}

export const LogFilter = ({
    isLoading,
    onChange,
    onSubmit,
}: LogFilterProps) => {
    const logLevels: string[] = ['Info', 'Debug', 'Warn', 'Error'];

    const [formState, setFormState] = useState<FormState>({
        values: {
            level: '',
            keyword: '',
        },
    });

    const handleFormChange = (
        event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>,
    ) => {
        const { name, value } = event.target;

        setFormState((prevState) => ({
            ...prevState,
            values: {
                ...prevState.values,
                [name]: value,
            },
        }));
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (onSubmit) {
            onSubmit(formState);
        }
    };

    useEffect(() => {
        if (onChange) {
            onChange(formState);
        }
    }, [formState]);

    return (
        <form className="box" onSubmit={handleSubmit}>
            <div className="field">
                <label className="label is-hidden-tablet">Filter</label>
                <div className="field-body">
                    <div className="field has-addons">
                        <div className="control">
                            <div className="select">
                                <select
                                    placeholder="Level"
                                    name="level"
                                    value={formState.values.level}
                                    onChange={handleFormChange}
                                >
                                    <option value="">All</option>
                                    {logLevels.map((level) => (
                                        <option key={level} value={level}>
                                            {level}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="control is-expanded">
                            <input
                                className="input"
                                name="keyword"
                                placeholder="Keyword"
                                value={formState.values.keyword}
                                onChange={handleFormChange}
                            />
                        </div>
                        <div className="control">
                            <button
                                className={`button ${
                                    isLoading ? 'is-loading' : ''
                                }`}
                                disabled={isLoading}
                                title="Search logs"
                            >
                                Search
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    );
};
