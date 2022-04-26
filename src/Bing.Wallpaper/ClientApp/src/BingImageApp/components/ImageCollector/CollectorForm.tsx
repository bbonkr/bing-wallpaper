import React, { useState } from 'react';

type FormState = {
    startIndex?: number;
    take?: number;
};

interface CollectorFormProps {
    isLoading?: boolean;
    onCollect?: (startIndex: number, take: number) => void;
}

export const CollectorForm = ({ isLoading, onCollect }: CollectorFormProps) => {
    const [formState, setFormState] = useState<FormState>({
        startIndex: 1,
        take: 8,
    });

    const handleChangeSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
        var value = e.currentTarget.value;
        var name = e.currentTarget.name;

        let numberValue: number | undefined = parseInt(value, 10);
        if (Number.isNaN(numberValue)) {
            numberValue = undefined;
        }

        setFormState((prevState) => ({
            ...(prevState ?? {}),
            [name]: numberValue ?? undefined,
        }));
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (formState && formState.startIndex && formState.take) {
            // console.info('formState', formState);
            if (onCollect) {
                onCollect(formState.startIndex, formState.take);
            }
        }
    };

    // useEffect(() => {
    //     console.info('formState', formState);
    // }, [formState]);

    return (
        <form className="box" onSubmit={handleSubmit}>
            <div className="field">
                <label className="label is-hidden-tablet">Collect</label>
                <div className="field-body">
                    <div className="columns is-mobile is-gapless">
                        <div className="column is-4">
                            <div
                                className={`select ${
                                    formState?.startIndex
                                        ? 'is-success'
                                        : 'is-danger'
                                }`}
                            >
                                <select
                                    placeholder="Select start index"
                                    name="startIndex"
                                    onChange={handleChangeSelect}
                                    value={formState?.startIndex}
                                >
                                    <option>Select start index</option>
                                    {Array(10)
                                        .fill(1)
                                        .map((x, index) => {
                                            return (
                                                <option
                                                    key={index}
                                                    value={index + x}
                                                >
                                                    {index + 1}
                                                </option>
                                            );
                                        })}
                                </select>
                            </div>
                        </div>
                        <div className="column is-4 ">
                            <div
                                className={`select ${
                                    formState?.take ? 'is-success' : 'is-danger'
                                }`}
                            >
                                <select
                                    placeholder="Select items count"
                                    name="take"
                                    onChange={handleChangeSelect}
                                    value={formState?.take}
                                >
                                    <option>Select items count</option>
                                    {Array(8)
                                        .fill(1)
                                        .map((x, index) => {
                                            return (
                                                <option
                                                    key={index}
                                                    value={index + x}
                                                >
                                                    {index + 1}
                                                </option>
                                            );
                                        })}
                                </select>
                            </div>
                        </div>
                        <div className="column is-4 ">
                            <button
                                type="submit"
                                className={`button ${
                                    isLoading ? 'is-loading' : ''
                                }`}
                                disabled={
                                    isLoading ||
                                    !formState?.startIndex ||
                                    !formState?.take
                                }
                                title="Collect images"
                            >
                                Collect
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    );
};
