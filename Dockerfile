FROM ubuntu:20.04
# To show errors and streams straightforward in the terminal without buffering.
ENV PYTHONUNBUFFERED=1
# To halt/supress any interactives questions in the terminal during installing packages.
ARG DEBIAN_FRONTEND=noninteractive
RUN apt-get update && apt-get install -y python3.10 python3-pip curl
RUN curl -sS https://bootstrap.pypa.io/get-pip.py
COPY reqs /reqs
RUN pip install -r reqs/requirements.txt 
COPY /src /usr/bin/src
CMD ["python3", "/usr/bin/src/comparison/evaluate.py"]
# 