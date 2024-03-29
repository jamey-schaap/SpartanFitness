import axios from "axios";
import { FormEvent, useContext, useEffect, useRef, useState } from "react";
import { Link, NavLink, useNavigate, useSearchParams } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import LogoSvg from "../../../../../assets/logos/svgs/logo.svg";
import LoadingIcon from "../../components/icons/LoadingIcon";
import AuthContext from "../../contexts/AuthProvider";
import AuthenticationResponse from "../../types/responses/AuthenticationResponse";
import { extractErrors } from "../../utils/ExtractErrors";
import { BsFillEyeSlashFill, BsFillEyeFill } from "react-icons/bs";

const LOGIN_ENDPOINT = `${import.meta.env.VITE_API_BASE}/auth/login`;

const LoginPage = () => {
  const { auth, setAuth } = useContext(AuthContext);
  const [searchParams] = useSearchParams();
  const returnToPath = searchParams.get("return_to") ?? "/";
  const emailRef = useRef<HTMLInputElement>();

  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState<string[] | null>(null);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [showPassword, setShowPassword] = useState(false);

  const [persist, setPersist] = useState<boolean>(
    JSON.parse(localStorage.getItem("persist") ?? "false") || false,
  );

  const togglePersist = () =>
    setPersist((prev) => {
      localStorage.setItem("persist", String(!prev));
      return !prev;
    });

  useEffect(() => {
    if (emailRef.current) {
      emailRef.current.focus();
    }
    if (auth.user != null) {
      navigate(returnToPath);
    }
  });

  const handleLogin = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    setIsLoading(true);
    setErrors(null);

    try {
      await axios
        .post<AuthenticationResponse>(
          LOGIN_ENDPOINT,
          { email: email, password: password },
          {
            headers: { "Content-Type": "application/json" },
          },
        )
        .then((res) => {
          if (res.data.id) {
            setAuth(res.data);
            navigate(returnToPath);
          }
        })
        .catch((err) => {
          setIsLoading(false);

          if (err.response.status === 500) {
            toast.error("Unable to reach the server", {
              toastId: err.code,
              position: "bottom-right",
              autoClose: 5000,
              hideProgressBar: false,
              closeOnClick: true,
              pauseOnHover: true,
              draggable: true,
              progress: undefined,
              theme: "colored",
            });
            setErrors([
              "The server is unreachable, please try again at a later time.",
            ]);
            return;
          }

          if (err.response.status === 400) {
            setErrors(extractErrors(err.response.data.errors));
          }
        });
    } catch {
      /* empty */
    }

    setEmail("");
    setPassword("");
    setIsLoading(false);
  };

  return (
    <>
      <div className="flex justify-center pt-28 pb-10">
        <div className="text-center">
          <Link to={"/"}>
            <img
              src={LogoSvg}
              className="w-[10rem] h-[10rem] mx-auto hover:rotate-8 hover:transition duration-150 ease-in-out"
              alt={"SpartanFitness Logo"}
            />
          </Link>
          <p>Sign in to SpartanFitness</p>
        </div>
      </div>

      <div className="flex justify-center pb-1">
        <form
          onSubmit={(e) => handleLogin(e)}
          id="login-form"
          className="w-full max-w-sm m-auto bg-semi-black shadow-xl rounded-lg px-8 pt-6 pb-5 mb-4 border border-gray"
        >
          <div className="mb-4">
            <label className="block text-white mb-2 ml-1">Email address</label>
            <input
              className="shadow appearance-none border border-gray rounded-lg w-full py-1.5 px-3 text-white leading-tight focus:outline focus:outline-blue focus:shadow-outline bg-black"
              id="email"
              type="text"
              placeholder="example@gmail.com"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              autoComplete="off"
            />
          </div>
          <div className="">
            <div className="relative">
              <label className="text-white mb-2 ml-1 flex items-center">
                Password *{" "}
                <span
                  className="ml-2 cursor-pointer hover:opacity-80"
                  onClick={() => setShowPassword((prev) => !prev)}
                >
                  {showPassword ? <BsFillEyeSlashFill /> : <BsFillEyeFill />}
                </span>
              </label>
              <NavLink
                to="/forgot-password"
                className="absolute top-0 right-0 text-blue"
              >
                Forgot Password?
              </NavLink>
            </div>
            <input
              className="shadow appearance-none border border-gray rounded-lg w-full py-1.5 px-3 text-white mb-3 leading-tight focus:outline focus:outline-blue focus:shadow-outline bg-black"
              id="password"
              type={showPassword ? "text" : "password"}
              placeholder="******************"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          <div className="mb-3">
            <div className="flex items-center ml-0.5" onClick={togglePersist}>
              <input
                type="checkbox"
                checked={persist}
                // eslint-disable-next-line @typescript-eslint/no-empty-function
                onChange={() => {}}
                className="w-4 h-4 border-gray rounded-xl accent-blue"
              />
              <label className="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300 hover:underline select-none cursor-pointer">
                Keep me signed in
              </label>
            </div>
          </div>

          <div className="flex items-center justify-between pb-1">
            <button
              className="bg-dark-green hover:bg-light-green text-white py-1.5 px-4 rounded-lg focus:outline-none focus:shadow-outline w-full block"
              type="submit"
              value="Submit"
              form="login-form"
            >
              {!isLoading && <p>Sign In</p>}
              {(isLoading || isLoading == undefined) && (
                <div className="flex items-center justify-center animate-pulse">
                  <LoadingIcon classNames="mr-2 text-white fill-white w-5 h-5" />
                  <p>Logging in...</p>
                </div>
              )}
            </button>
          </div>
        </form>
      </div>

      {errors && (
        <div className="flex justify-center pb-4">
          <div className="shadow appearance-none border border-red rounded-lg w-full py-3 px-3 text-whiteas bg-black font-medium max-w-sm text-center">
            {errors.map((e, i) => (
              <p key={`error-${i}`}>{e}</p>
            ))}
          </div>
        </div>
      )}

      <div className="flex justify-center">
        <p className="px-10 pt-5 pb-5 mb-4 border border-gray max-w-sm rounded-lg">
          Not a Spartan yet?{" "}
          <Link to="/signup" className="text-blue">
            Create an account
          </Link>
          .
        </p>
      </div>
    </>
  );
};

export default LoginPage;
