import axios from "axios";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import SearchParamsFactory from "../utils/SearchParamsFactory";
import Exception from "../types/domain/Exception";
import Page from "../types/domain/Page";
import Workout from "../types/domain/Workout";

const WORKOUT_ENDPOINT = `${
  import.meta.env.VITE_API_BASE
}/coaches/all/workouts/page`;

type ApiResponse = Omit<Page<Workout>, "values"> & {
  workouts: Workout[];
};

type PageArguments = {
  page?: number;
  size?: number;
  sort?: string;
  order?: string;
  query?: string;
};

const useWorkoutsPage = ({
  page,
  size,
  sort,
  order,
  query,
}: PageArguments): [
  Page<Workout> | undefined,
  Exception | undefined,
  boolean,
] => {
  const [workoutsPage, setWorkoutsPage] = useState<Page<Workout>>();
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
    const fetchWorkouts = async () => {
      setIsLoading(true);

      try {
        await axios
          .get<ApiResponse>(`${WORKOUT_ENDPOINT}${queryString}`, {
            headers: {
              Accept: "application/json",
              Authorization: `bearer ${localStorage.getItem("token")}`,
            },
          })
          .then((res) => {
            setWorkoutsPage({ ...res.data, values: res.data.workouts });
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

    fetchWorkouts();
  }, [page, size, sort, order, query]);

  return [workoutsPage, error, isLoading];
};

export default useWorkoutsPage;
