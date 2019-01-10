from flask import Flask, request, jsonify

app = Flask(__name__)


@app.route('/', methods=['GET', 'POST'])
def hello():
    if request.method == 'POST':
        data = request.form['data']
        with open('data.txt', 'w') as f:
            f.write(data)
    else:
        with open('data.txt', 'r') as f:
            data = f.read()

    return jsonify({'data': data})


if __name__ == '__main__':
    app.run(debug=True)
