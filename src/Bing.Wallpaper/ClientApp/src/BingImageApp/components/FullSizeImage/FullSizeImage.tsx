import React, { useEffect, useRef } from 'react';
import { useImagesApi } from '../../hooks/useImagesApi';
import { FaExpandArrowsAlt, FaTimes } from 'react-icons/fa';
import { Image } from '../Image';
import './style.css';

export const FullSizeImage = () => {
    const { fullSizeImage, hideFullSizeImage } = useImagesApi();
    const imageRef = useRef<HTMLImageElement>(null);

    const handleClickClose = () => {
        hideFullSizeImage();
    };

    const handleClickFullScreen = () => {
        imageRef.current?.requestFullscreen();
    };

    useEffect(() => {
        const handleKeyUp = (ev: KeyboardEvent) => {
            if (ev.key === 'Escape') {
                hideFullSizeImage();
            }
        };

        if (fullSizeImage) {
            window.addEventListener('keyup', handleKeyUp);
        } else {
            window.removeEventListener('keyup', handleKeyUp);
        }

        return () => {
            window.removeEventListener('keyup', handleKeyUp);
        };
    }, [fullSizeImage]);

    return (
        fullSizeImage && (
            <div className="full-size-image-container">
                <div className="tools">
                    <div>
                        <button
                            className="button"
                            title="Full screen"
                            onClick={handleClickFullScreen}
                        >
                            <FaExpandArrowsAlt />
                        </button>
                    </div>
                    <div>
                        <p>{fullSizeImage.title ?? fullSizeImage.fileName}</p>
                    </div>
                    <div>
                        <button
                            className="button"
                            title="Close"
                            onClick={handleClickClose}
                        >
                            <FaTimes />
                        </button>
                    </div>
                </div>
                <figure>
                    <Image
                        imageSrc={`/api/v1.0/files/${encodeURIComponent(
                            fullSizeImage.fileName ?? '',
                        )}`}
                        imageThumbnailSrc={`/api/v1.0/files/${encodeURIComponent(
                            fullSizeImage.fileName ?? '',
                        )}?type=thumbnail`}
                        imgProps={{
                            title: fullSizeImage.title ?? '',
                            alt: fullSizeImage?.fileName ?? '',
                        }}
                    />
                    {fullSizeImage.copyright && (
                        <figcaption>
                            {fullSizeImage.copyrightLink ? (
                                <p>
                                    <a
                                        href={fullSizeImage.copyrightLink}
                                        target="_blank"
                                        rel="nofollow"
                                    >
                                        {fullSizeImage.copyright}
                                    </a>
                                </p>
                            ) : (
                                <p>{fullSizeImage.copyright}</p>
                            )}
                        </figcaption>
                    )}
                </figure>
            </div>
        )
    );
};
