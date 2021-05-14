import React, { useRef, useEffect, useState } from 'react';
import { useImagesApi } from '../../hooks/useImagesApi';
import { ImageItemModel } from '../../models';
import './style.css';

interface ImageCardProps {
    image: ImageItemModel;
}

export const ImageCard = ({ image }: ImageCardProps) => {
    const { showFullSizeImage } = useImagesApi();
    const [imageLoaded, setImageLoaded] = useState(false);

    const imageRef = useRef<HTMLImageElement>(null);
    const observerRef = useRef<IntersectionObserver>();

    const handleIntersection = (
        entries: IntersectionObserverEntry[],
        intersectionObserver: IntersectionObserver,
    ) => {
        entries.forEach((entry) => {
            if (entry.isIntersecting) {
                intersectionObserver.unobserve(entry.target);
                setImageLoaded((prevState) => true);
                console.info(
                    '🔨 Request image file: ',
                    imageRef?.current?.src ||
                        imageRef?.current?.srcset ||
                        'Does not find image file uri',
                );
            }
        });
    };

    useEffect(() => {
        const imgEl = imageRef.current;

        if (!observerRef.current) {
            observerRef.current = new IntersectionObserver(handleIntersection, {
                threshold: 0.3,
            });
        }

        if (imgEl) {
            observerRef.current.observe(imgEl);
        }

        return () => {
            if (observerRef.current) {
                observerRef.current.disconnect();
            }
        };
    }, []);

    const handleClickImage = () => {
        showFullSizeImage(image);
    };
    return (
        <div
            className="column is-two-thirds-tablet is-half-desktop is-one-third-widescreen is-one-quarter-fullhd clickable is-position-relative"
            onClick={handleClickImage}
        >
            <img
                ref={imageRef}
                src={
                    imageLoaded
                        ? `/api/v1.0/files/${encodeURIComponent(
                              `${image.fileName}`,
                          )}`
                        : `/api/v1.0/files/${encodeURIComponent(
                              `${image.fileName}`,
                          )}?type=thumbnail`
                }
                className={imageLoaded ? 'loaded' : ''}
                title={image.title ?? image.fileName}
                alt={image.title ?? image.fileName}
            />
            <div className="card-title-over-image rounded-bottom">
                <p>{image.title ?? image.fileName}</p>
            </div>
        </div>
    );
};
