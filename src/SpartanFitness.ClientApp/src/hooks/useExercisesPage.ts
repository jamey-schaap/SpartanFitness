import axios from "axios";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import Page from "../types/domain/Page";
import Exception from "../types/domain/Exception";
import Exercise from "../types/domain/Exercise";
import SearchParamsFactory from "../types/SearchParamsFactory";

const EXERCISE_ENDPOINT = `${import.meta.env.VITE_API_BASE}/exercises/page`;

export type ExercisesPage = { exercises: Exercise[] } & Page;

const useExercisesPage = (
  page?: number,
  size?: number,
  sort?: string,
  order?: string,
  query?: string,
): [ExercisesPage | undefined, Exception | undefined, boolean] => {
  const [exercisesPage, setExercisePage] = useState<ExercisesPage>();
  const [error, setError] = useState<Exception>();
  const [isLoading, setIsLoading] = useState(false);

  const queryString = SearchParamsFactory.CreateQueryString(
    page,
    size,
    sort,
    order,
    query,
  );

  useEffect(() => {
    const fetchExercises = async () => {
      setIsLoading(true);

      try {
        await axios
          .get<ExercisesPage>(`${EXERCISE_ENDPOINT}${queryString}`, {
            headers: {
              Accept: "application/json",
              Authorization: `bearer ${localStorage.getItem("token")}`,
            },
          })
          .then((res) => {
            setExercisePage(res.data);
            setIsLoading(false);
          })
          .catch((err) => {
            setIsLoading(false);
            setError({
              message: err.message,
              code: err.code,
            });
            toast.error(
              err.code == "ERR_NETWORK"
                ? "Unable to reach the server"
                : err.response.statusText,
              {
                toastId: err.code,
                position: "bottom-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
              },
            );
          });
      } catch {
        /* empty */
      }
    };

    fetchExercises();
  }, [page, size, sort, order, query]);

  return [exercisesPage, error, isLoading];
};

export default useExercisesPage;