import React, { useState } from 'react';

const CornFarm = () => {
  const [cornCount, setCornCount] = useState(0);
  const [username, setUsername] = useState('');
  const [message, setMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [loading, setLoading] = useState(false);

  const handleUsernameChange = (e) => {
    setUsername(e.target.value);
  };

  const handleBuyCorn = async () => {
    if (!username.trim()) {
      setErrorMessage('Please provide a username!');
      return;
    }

    setLoading(true);
    setMessage('');
    setErrorMessage('');

    try {
      const response = await fetch('http://localhost:5123/api/corn/buy', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Username: username }),
      });
      if (response.ok) {
        const responseData = await response.json();
        setCornCount(cornCount + 1);
        setMessage(`Successfully bought corn! 🌽 You now have ${responseData.cornCount} corn.`);
      } else if (response.status === 429) {
        setErrorMessage('Too many requests. Please wait a minute before buying more corn.');
      } else {
        setErrorMessage('Error purchasing corn. Please try again.');
      }
    } catch (error) {
      setErrorMessage('Error purchasing corn. Please try again.');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container">
      <h1>Corn Farm</h1>
      <div>
        <label htmlFor="username">Username</label>
        <input
          type="text"
          id="username"
          value={username}
          onChange={handleUsernameChange}
          placeholder="Enter your username"
        />
      </div>
      <button onClick={handleBuyCorn} disabled={loading}>
        {loading ? 'Buying...' : 'Buy Corn'}
      </button>
      {message && <p className="message success">{message}</p>}
      {errorMessage && <p className="message error">{errorMessage}</p>}
    </div>
  );
};

export default CornFarm;
